using Calculations.Abstractions;

namespace Calculations.Models
{
    public class DictionaryVariableValueProvider : IVariableValueProvider
    {
        private readonly IReadOnlyDictionary<string, decimal> _dictionary;

        public DictionaryVariableValueProvider(IReadOnlyDictionary<string, decimal> dictionary)
        {
            _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        public bool TryGetVariableValue(string variable, out decimal value)
        {
            return _dictionary.TryGetValue(variable, out value);
        }
    }
}
