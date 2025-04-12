using System.Collections.Immutable;
using Calculations.Abstractions;
using Calculations.Models.Ast;

namespace Calculations.Models.Commands
{
    internal class CmdBuilder
    {
        private readonly AstBaseNode _astRoot;
        private readonly HashSet<string> _variables;

        public static ICmdInvoker BuildCmdInvoker(AstBaseNode astRoot)
        {
            var builder = new CmdBuilder(astRoot);
            return builder.Build();
        }

        private CmdBuilder(AstBaseNode astRoot)
        {
            _astRoot = astRoot ?? throw new ArgumentNullException(nameof(astRoot));
            _variables = new HashSet<string>();
        }

        public ICmdInvoker Build()
        {
            var cmd = CreateCommand(_astRoot);
            return new CmdInvoker(
                _variables.ToImmutableList(),
                cmd);
        }

        private CmdBase CreateCommand(AstBaseNode astNode)
        {
            switch (astNode)
            {
                case AstBinOperator bin:
                    return new CmdBinOperator(
                        bin.OperatorType,
                        CreateCommand(bin.LeftOperand),
                        CreateCommand(bin.RightOperand));

                case AstUnaryMinusOperator minus:
                    return new CmdUnaryMinus(CreateCommand(minus.Operand));

                case AstConstValueNode constant:
                    return new CmdConstant(constant.Value);

                case AstVariableNode variable:
                    if (!_variables.Contains(variable.Name))
                        _variables.Add(variable.Name);
                    return new CmdVariable(variable.Name);

                default:
                    throw new NotImplementedException($"Node type '{astNode.GetType()}'");
            }
        }
    }
}
