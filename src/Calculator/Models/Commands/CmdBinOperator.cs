using Calculations.Abstractions;

namespace Calculations.Models.Commands
{
    internal class CmdBinOperator : CmdBase
    {
        private readonly BinOperatorType _op;
        private readonly CmdBase _left;
        private readonly CmdBase _right;

        public CmdBinOperator(BinOperatorType op, CmdBase left, CmdBase right)
        {
            _op = op;
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override decimal Calculate(IVariableValueProvider variables)
        {
            switch (_op)
            {
                case BinOperatorType.Plus:
                    return _left.Calculate(variables) + _right.Calculate(variables);

                case BinOperatorType.Minus:
                    return _left.Calculate(variables) - _right.Calculate(variables);

                case BinOperatorType.Mult:
                    return _left.Calculate(variables) * _right.Calculate(variables);

                case BinOperatorType.Div:
                    return _left.Calculate(variables) / _right.Calculate(variables);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
