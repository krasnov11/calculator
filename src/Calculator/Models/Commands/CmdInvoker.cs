using Calculations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations.Models.Commands
{
    internal class CmdInvoker : ICmdInvoker
    {
        private readonly CmdBase _command;

        public CmdInvoker(IReadOnlyList<string> variables, CmdBase command)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            Variables = variables;
        }

        public decimal Calculate(IVariableValueProvider variables)
        {
            return _command.Calculate(variables);
        }

        public IReadOnlyList<string> Variables { get; }
    }
}
