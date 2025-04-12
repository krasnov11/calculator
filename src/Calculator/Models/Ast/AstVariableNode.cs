using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Models.Ast
{
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
