namespace Calculations.Models.Ast
{
    /// <summary>
    /// bin operator
    /// </summary>
    internal class AstBinOperator : AstBaseNode
    {
        public AstBinOperator(BinOperatorType operatorType, AstBaseNode leftOperand, AstBaseNode rightOperand)
        {
            OperatorType = operatorType;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        public BinOperatorType OperatorType { get; }

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
                case BinOperatorType.Plus: op = '+'; break;
                case BinOperatorType.Minus: op = '-'; break;
                case BinOperatorType.Mult: op = '*'; break;
                case BinOperatorType.Div: op = '/'; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return $"({LeftOperand} {op} {RightOperand})";
        }
    }
}
