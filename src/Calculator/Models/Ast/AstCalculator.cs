using Calculations.Models.Scan;
using System.Globalization;

namespace Calculations.Models.Ast
{
    public class AstCalculator
    {
        private readonly IReadOnlyList<TokenInfo> _tokens;
        private int _tokenIndex;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="tokenSequence">Токены</param>
        public AstCalculator(IReadOnlyList<TokenInfo> tokenSequence)
        {
            _tokens = tokenSequence ?? throw new ArgumentNullException(nameof(tokenSequence));
        }

        public AstBaseNode GetAst()
        {
            var value = CalcExpression();

            if (NextToken())
                throw new InvalidOperationException("There are rest of expression");

            return value;
        }

        private AstBaseNode CalcExpression()
        {
            return null;

            /*var values = new List<decimal>();
            var operators = new List<string>();

            var op1 = CalculateOperand();
            values.Add(op1);

            while (IfNextToken(Tokens.Operator))
            {
                NextToken();
                var op = CurrentValue();

                operators.Add(op);
                op1 = CalculateOperand();
                values.Add(op1);
            }

            while (operators.Count > 0)
            {
                var i = 0;
                while (i < operators.Count)
                {
                    var oper = operators[i];
                    switch (oper)
                    {
                        case "*":
                            values[i] *= values[i + 1];
                            values.RemoveAt(i + 1);
                            operators.RemoveAt(i);
                            break;

                        case "/":
                            values[i] /= values[i + 1];
                            values.RemoveAt(i + 1);
                            operators.RemoveAt(i);
                            break;

                        default:
                            ++i;
                            break;
                    }
                }

                while (operators.Count > 0)
                {
                    var oper = operators[0];
                    switch (oper)
                    {
                        case "+":
                            values[0] += values[1];
                            values.RemoveAt(1);
                            operators.RemoveAt(0);
                            break;

                        case "-":
                            values[0] -= values[1];
                            values.RemoveAt(1);
                            operators.RemoveAt(0);
                            break;

                        default:
                            throw new InvalidOperationException($"Unexpected operator '{oper}'");
                    }
                }
            }

            return values[0];*/
        }

        private decimal CalculateOperand()
        {
            return 0;

            /*if (!NextToken())
                throw new InvalidOperationException("Missing operand");

            var sign = 1;
            if (IfToken(Tokens.Operator))
            {
                var oper = CurrentValue();
                switch (oper)
                {
                    case "+": break;
                    case "-": sign = -1; break;
                    default:
                        throw new InvalidOperationException($"Unexpected operator '{oper}'");
                }

                if (!NextToken())
                    throw new InvalidOperationException("Missing operand");
            }

            switch (CurrentToken())
            {
                case Tokens.Number:
                    return sign * CalcNumber();

                case Tokens.Variable:
                    return sign * CalcVariable();

                case Tokens.LeftBracket:
                    var expResult = CalcExpression();
                    if (!NextToken())
                        throw new InvalidOperationException("Expected ')'");
                    RequiredToken(Tokens.RightBracket);
                    return sign * expResult;

                default:
                    throw new InvalidOperationException($"Unexpected token '{CurrentToken()}'");
            }*/
        }

        private decimal CalcVariable()
        {
            return 0;

            /*var varName = CurrentValue();
            if (!_variables.TryGetValue(varName, out var value))
                throw new InvalidOperationException($"Variable '{varName}' is not specified");

            return value;*/
        }

        private decimal CalcNumber()
        {
            if (!decimal.TryParse(
                    CurrentValue(), NumberStyles.Float | NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out var result))
                throw new InvalidOperationException($"Can't parse value '{CurrentValue()}' as decimal");

            return result;
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
