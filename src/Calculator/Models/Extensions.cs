using Calculations.Models.Ast;
using Calculations.Models.Scan;

namespace Calculations.Models
{
    public static class Extensions
    {
        public static IReadOnlyList<TokenInfo> GetCalculatorTokens(this string text)
        {
            var tokens = new List<TokenInfo>();

            var scanner = new Scanner(text);
            while (scanner.Next())
            {
                tokens.Add(
                    new TokenInfo(scanner.GetToken(), scanner.GetTokenValue()));
            }

            if (scanner.HasErrors || tokens.Count == 0)
                throw new InvalidOperationException($"Invalid expression: '{text}'");

            return tokens.AsReadOnly();
        }
    }
}
