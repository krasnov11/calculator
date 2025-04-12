using System.Globalization;

namespace Calculations.Models.Ast
{
    /// <summary>
    /// const
    /// </summary>
    internal class AstConstValueNode : AstBaseNode
    {
        public AstConstValueNode(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
