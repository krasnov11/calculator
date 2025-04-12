using Calculations.Abstractions;

namespace Calculations.Models.Commands
{
    internal class CmdVariable : CmdBase
    {
        private readonly IVariableValueProvider _prov;
        private readonly string _name;

        public CmdVariable(IVariableValueProvider prov, string name)
        {
            _prov = prov ?? throw new ArgumentNullException(nameof(prov));
            _name = name ?? throw new ArgumentNullException(nameof(prov));
        }

        public override decimal Calculate()
        {
            if (_prov.TryGetVariableValue(_name, out var value))
                return value;

            throw new InvalidOperationException($"Value of variable '{_name}' is not found");
        }
    }
}
