using Calculations.Models.Scan;
using System.Globalization;
using Calculations.Models;
using Calculations.Models.Ast;
using Calculations.Models.Commands;
using Calculations.Abstractions;

namespace Calculations
{
    /// <summary>
    /// Класс простого калькулятора
    /// </summary>
    public class Calculator
    {
        private readonly CmdBase _cmd;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">Строка выражения</param>
        /// <exception cref="InvalidOperationException"></exception>
        public Calculator(string text)
        {
            var tokens = text.GetCalculatorTokens();
            var ast = AstBuilder.BuildAst(tokens);
            _cmd = CmdBuilder.Build(ast);
        }

        /// <summary>
        /// Вячислить значение
        /// </summary>
        /// <param name="variables">Переменные</param>
        /// <returns></returns>
        public decimal Calculate(IReadOnlyDictionary<string, decimal> variables)
        {
            return _cmd.Calculate(variables.AsVariableValueProvider());
        }

        /// <summary>
        /// Вячислить значение
        /// </summary>
        /// <param name="variables">Переменные</param>
        /// <returns></returns>
        public decimal Calculate(IVariableValueProvider? variables = null)
        {
            return _cmd.Calculate(variables ?? EmptyVariableValueProvider.Instance);
        }
    }
}
