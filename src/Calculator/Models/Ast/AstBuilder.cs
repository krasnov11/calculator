﻿using Calculations.Models.Scan;
using System.Globalization;

namespace Calculations.Models.Ast
{
    internal class AstBuilder
    {
        private readonly IReadOnlyList<AstTokenInfo> _tokens;
        private int _tokenIndex;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="tokenSequence">Токены</param>
        private AstBuilder(IReadOnlyList<AstTokenInfo> tokenSequence)
        {
            _tokens = tokenSequence ?? throw new ArgumentNullException(nameof(tokenSequence));
        }

        public static AstBaseNode BuildAst(IReadOnlyList<AstTokenInfo> tokenSequence)
        {
            var builder = new AstBuilder(tokenSequence);
            return builder.Build();
        }

        public AstBaseNode Build()
        {
            _tokenIndex = -1;
            var value = AstExpression();

            if (NextToken())
                throw new InvalidOperationException("There are rest of expression");

            return value;
        }

        private AstBaseNode AstExpression()
        {
            var root = AstOperand();
            AstBinOperator? lastFoundOperator = null;

            while (IfNextToken(Tokens.Operator))
            {
                NextToken();
                var op = CurrentValue();

                var nextOperand = AstOperand();

                switch (op)
                {
                    case "+":
                        // new root
                        lastFoundOperator = new AstBinOperator(
                            BinOperatorType.Plus,
                            root,
                            nextOperand);
                        root = lastFoundOperator;
                        break;

                    case "-":
                        // new root
                        lastFoundOperator = new AstBinOperator(
                            BinOperatorType.Minus,
                            root,
                            nextOperand);
                        root = lastFoundOperator;
                        break;

                    case "*":
                        if (lastFoundOperator == null)
                        {
                            // new root
                            lastFoundOperator = new AstBinOperator(
                                BinOperatorType.Mult,
                                root,
                                nextOperand);
                            root = lastFoundOperator;
                        }
                        else
                        {
                            // modify last leafs

                            var newOper = new AstBinOperator(
                                BinOperatorType.Mult,
                                lastFoundOperator.RightOperand,
                                nextOperand);

                            lastFoundOperator.SetRightOperand(newOper);
                            lastFoundOperator = newOper;
                        }
                        break;

                    case "/":
                        if (lastFoundOperator == null)
                        {
                            // new root
                            lastFoundOperator = new AstBinOperator(
                                BinOperatorType.Div,
                                root,
                                nextOperand);
                            root = lastFoundOperator;
                        }
                        else
                        {
                            // modify last leafs

                            var newOper = new AstBinOperator(
                                BinOperatorType.Div,
                                lastFoundOperator.RightOperand,
                                nextOperand);

                            lastFoundOperator.SetRightOperand(newOper);
                            lastFoundOperator = newOper;
                        }
                        break;

                    default:
                        throw new NotSupportedException($"Operator '{op}' is not supported");
                }
            }

            return root;
        }

        private AstBaseNode AstOperand()
        {
            if (!NextToken())
                throw new InvalidOperationException("Missing operand");

            var unaryMinus = false;
            if (IfToken(Tokens.Operator))
            {
                var oper = CurrentValue();
                switch (oper)
                {
                    case "+": break;
                    case "-": unaryMinus = true; break;
                    default:
                        throw new InvalidOperationException($"Unexpected operator '{oper}'");
                }

                if (!NextToken())
                    throw new InvalidOperationException("Missing operand");
            }

            switch (CurrentToken())
            {
                case Tokens.Number:
                    return unaryMinus 
                        ? new AstUnaryMinusOperator(AstConstant())
                        : AstConstant();

                case Tokens.Variable:
                    return unaryMinus 
                        ? new AstUnaryMinusOperator(AstVariable())
                        : AstVariable();

                case Tokens.LeftBracket:
                    var expResult = AstExpression();
                    if (!NextToken())
                        throw new InvalidOperationException("Expected ')'");
                    RequiredToken(Tokens.RightBracket);
                    return unaryMinus 
                        ? new AstUnaryMinusOperator(expResult)
                        : expResult;

                default:
                    throw new InvalidOperationException($"Unexpected token '{CurrentToken()}'");
            }
        }

        private AstVariableNode AstVariable()
        {
            var varName = CurrentValue();
            return new AstVariableNode(varName);
        }

        private AstConstValueNode AstConstant()
        {
            if (!decimal.TryParse(
                    CurrentValue(), NumberStyles.Float | NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out var result))
                throw new InvalidOperationException($"Can't parse value '{CurrentValue()}' as decimal");

            return new AstConstValueNode(result);
        }

        private bool NextToken()
        {
            _tokenIndex++;
            return CanReadCurrent();
        }

        private bool CanReadCurrent() => _tokenIndex < _tokens.Count;
        private Tokens CurrentToken() => _tokens[_tokenIndex].Token;
        private string CurrentValue() => _tokens[_tokenIndex].Value;

        private bool IfToken(Tokens token)
        {
            if (!CanReadCurrent())
                return false;

            return token == CurrentToken();
        }

        private bool IfNextToken(Tokens token)
        {
            var nextTokenInd = _tokenIndex + 1;

            if (nextTokenInd >= _tokens.Count)
                return false;

            return _tokens[nextTokenInd].Token == token;
        }

        private void RequiredToken(Tokens token)
        {
            if (!IfToken(token))
            {
                throw new InvalidOperationException($"Expected token '{token}' but actual is '{CurrentToken()}' (value = '{CurrentValue()}')");
            }
        }
    }
}
