using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Models.Ast
{
    /// <summary>
    /// unary operator
    /// </summary>
    public class AstUnaryMinusOperator : AstBaseNode
    {
        public AstUnaryMinusOperator(AstBaseNode operand)
        {
            Operand = operand;
        }

        public AstBaseNode Operand { get; }

        public override string ToString()
        {
            return $"-{Operand}";
        }
    }
}
