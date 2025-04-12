using System.Globalization;

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
    public class AstUnaryMinusOperator : AstNodeOperator
    {
        public AstUnaryMinusOperator(AstBaseNode operand)
        {
            Operand = operand;
        }

        public AstBaseNode Operand { get; }

        public override IEnumerable<AstBaseNode> GetChild()
        {
            yield return Operand;
        }

        public override string ToString()
        {
            return $"-{Operand}";
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
        public AstBaseNode RightOperand { get; private set; }

        public override IEnumerable<AstBaseNode> GetChild()
        {
            yield return LeftOperand;
            yield return RightOperand;
        }

        public void SetRightOperand(AstBaseNode node)
        {
            RightOperand = node ?? throw new ArgumentNullException(nameof(node));
        }

        public override string ToString()
        {
            var op = ' ';
            switch (OperatorType)
            {
                case AstBinaryOperatorType.Plus: op = '+'; break;
                case AstBinaryOperatorType.Minus: op = '-'; break;
                case AstBinaryOperatorType.Mult: op = '*'; break;
                case AstBinaryOperatorType.Div: op = '/'; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return $"({LeftOperand} {op} {RightOperand})";
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

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
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

        public override string ToString()
        {
            return Name;
        }
    }
}
