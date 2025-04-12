using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Models.Ast
{
    /// <summary>
    /// Бинарный оператор
    /// </summary>
    public enum AstBinOperatorType
    {
        /// <summary>
        /// operator +
        /// </summary>
        Plus,

        /// <summary>
        /// operator -
        /// </summary>
        Minus,

        /// <summary>
        /// operator *
        /// </summary>
        Mult,

        /// <summary>
        /// operator /
        /// </summary>
        Div
    }
}
