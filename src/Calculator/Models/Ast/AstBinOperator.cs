using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Models.Ast
{
    /// <summary>
    /// bin operator
    /// </summary>
    public class AstBinOperator : AstBaseNode
    {
        public AstBinOperator(AstBinOperatorType operatorType, AstBaseNode leftOperand, AstBaseNode rightOperand)
        {
            OperatorType = operatorType;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        public AstBinOperatorType OperatorType { get; }

        public AstBaseNode LeftOperand { get; }
        public AstBaseNode RightOperand { get; private set; }

        public void SetRightOperand(AstBaseNode node)
        {
            RightOperand = node ?? throw new ArgumentNullException(nameof(node));
        }

        public override string ToString()
        {
            var op = ' ';
            switch (OperatorType)
            {
                case AstBinOperatorType.Plus: op = '+'; break;
                case AstBinOperatorType.Minus: op = '-'; break;
                case AstBinOperatorType.Mult: op = '*'; break;
                case AstBinOperatorType.Div: op = '/'; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return $"({LeftOperand} {op} {RightOperand})";
        }
    }
}
