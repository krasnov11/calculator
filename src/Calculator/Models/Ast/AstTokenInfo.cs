using Calculations.Models.Scan;

namespace Calculations.Models.Ast
{
    internal struct AstTokenInfo
    {
        public readonly Tokens Token;
        public readonly string Value;

        public AstTokenInfo(Tokens token, string value)
        {
            Token = token;
            Value = value;
        }
    }
}
