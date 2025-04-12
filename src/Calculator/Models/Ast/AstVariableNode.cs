namespace Calculations.Models.Ast
{
    /// <summary>
    /// variable
    /// </summary>
    internal class AstVariableNode : AstBaseNode
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
