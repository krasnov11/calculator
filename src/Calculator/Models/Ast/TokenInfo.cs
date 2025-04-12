using Calculations.Models.Scan;

namespace Calculations.Models.Ast
{
    public struct TokenInfo
    {
        public readonly Tokens Token;
        public readonly string Value;

        public TokenInfo(Tokens token, string value)
        {
            Token = token;
            Value = value;
        }
    }
}
