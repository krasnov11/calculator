using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Models.Ast
{
    /// <summary>
    /// const
    /// </summary>
    public class AstConstValueNode : AstBaseNode
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
