using Calculations.Abstractions;

namespace Calculations.Models.Commands
{
    internal class CmdUnaryMinus : CmdBase
    {
        private readonly CmdBase _inner;

        public CmdUnaryMinus(CmdBase inner)
        {
            _inner = inner;
        }

        public override decimal Calculate(IVariableValueProvider variables)
        {
            return -1m * _inner.Calculate(variables);
        }
    }
}
