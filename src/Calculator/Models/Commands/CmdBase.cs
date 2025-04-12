using Calculations.Abstractions;

namespace Calculations.Models.Commands
{
    internal abstract class CmdBase
    {
        public abstract decimal Calculate(IVariableValueProvider variables);
    }
}
