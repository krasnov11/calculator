using Calculations.Abstractions;
using Calculations.Models.Ast;

namespace Calculations.Models.Commands
{
    internal class CmdBuilder
    {
        private readonly AstBaseNode _astRoot;

        public static CmdBase Build(AstBaseNode astRoot)
        {
            var builder = new CmdBuilder(astRoot);
            return builder.BuildCommand();
        }

        private CmdBuilder(AstBaseNode astRoot)
        {
            _astRoot = astRoot ?? throw new ArgumentNullException(nameof(astRoot));
        }

        public CmdBase BuildCommand()
        {
            return CreateCommand(_astRoot);
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
                    return new CmdVariable(variable.Name);

                default:
                    throw new NotImplementedException($"Node type '{astNode.GetType()}'");
            }
        }
    }
}
