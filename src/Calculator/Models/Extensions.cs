using Calculations.Abstractions;
using Calculations.Models.Ast;
using Calculations.Models.Scan;

namespace Calculations.Models
{
    public static class Extensions
    {
        internal static IReadOnlyList<AstTokenInfo> GetCalculatorTokens(this string text)
        {
            var tokens = new List<AstTokenInfo>();

            var scanner = new Scanner(text);
            while (scanner.Next())
            {
                tokens.Add(
                    new AstTokenInfo(scanner.GetToken(), scanner.GetTokenValue()));
            }

            if (scanner.HasErrors || tokens.Count == 0)
                throw new InvalidOperationException($"Invalid expression: '{text}'");

            return tokens.AsReadOnly();
        }

        public static IVariableValueProvider AsVariableValueProvider(this IReadOnlyDictionary<string, decimal>? vars)
        {
            return vars == null 
                ? EmptyVariableValueProvider.Instance
                : new DictionaryVariableValueProvider(vars);
        }
    }
}
