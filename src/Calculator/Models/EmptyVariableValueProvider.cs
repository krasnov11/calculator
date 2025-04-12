using Calculations.Abstractions;

namespace Calculations.Models
{
    public class EmptyVariableValueProvider : IVariableValueProvider
    {
        public static readonly EmptyVariableValueProvider Instance = new EmptyVariableValueProvider();

        private EmptyVariableValueProvider() { }

        public bool TryGetVariableValue(string variable, out decimal value)
        {
            value = 0;
            return false;
        }
    }
}
