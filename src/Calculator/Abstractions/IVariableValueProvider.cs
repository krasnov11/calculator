namespace Calculations.Abstractions
{
    public interface IVariableValueProvider
    {
        bool TryGetVariableValue(string variable, out decimal value);
    }
}
