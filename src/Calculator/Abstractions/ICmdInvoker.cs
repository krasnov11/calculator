using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Abstractions
{
    public interface ICmdInvoker
    {
        decimal Calculate(IVariableValueProvider variables);
        IReadOnlyList<string> Variables { get; }
    }
}
