using Calculations.Abstractions;

namespace Calculations.Models.Commands
{
    internal class CmdVariable : CmdBase
    {
        private readonly string _name;

        public CmdVariable(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override decimal Calculate(IVariableValueProvider variables)
        {
            if (variables?.TryGetVariableValue(_name, out var value) == true)
                return value;

            throw new InvalidOperationException($"Value of variable '{_name}' is not found");
        }
    }
}
