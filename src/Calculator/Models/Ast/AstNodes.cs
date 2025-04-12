namespace Calculations.Models.Ast
{
    /// <summary>
    /// base node
    /// </summary>
    public abstract class AstBaseNode
    {

    }

    /// <summary>
    /// base operator node
    /// </summary>
    public abstract class AstNodeOperator : AstBaseNode
    {
        public abstract IEnumerable<AstBaseNode> GetChild();
    }

    /// <summary>
    /// unary operator
    /// </summary>
    public class AstUnaryOperator : AstNodeOperator
    {
        public AstUnaryOperator(AstUnaryOperatorType operatorType, AstBaseNode operand)
        {
            OperatorType = operatorType;
            Operand = operand;
        }

        public AstUnaryOperatorType OperatorType { get; }

        public AstBaseNode Operand { get; }

        public override IEnumerable<AstBaseNode> GetChild()
        {
            yield return Operand;
        }
    }

    /// <summary>
    /// bin operator
    /// </summary>
    public class AstBinaryOperator : AstNodeOperator
    {
        public AstBinaryOperator(AstBinaryOperatorType operatorType, AstBaseNode leftOperand, AstBaseNode rightOperand)
        {
            OperatorType = operatorType;
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        public AstBinaryOperatorType OperatorType { get; }

        public AstBaseNode LeftOperand { get; }
        public AstBaseNode RightOperand { get; }

        public override IEnumerable<AstBaseNode> GetChild()
        {
            yield return LeftOperand;
            yield return RightOperand;
        }
    }

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
    }

    /// <summary>
    /// variable
    /// </summary>
    public class AstVariableNode : AstBaseNode
    {
        public AstVariableNode(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
