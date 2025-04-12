namespace Calculations.Models.Ast
{
    /// <summary>
    /// unary operator
    /// </summary>
    internal class AstUnaryMinusOperator : AstBaseNode
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
