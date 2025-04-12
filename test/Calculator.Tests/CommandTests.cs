using System;
using System.Collections.Generic;
using System.Globalization;
using Calculations.Models;
using Calculations.Models.Ast;
using Calculations.Models.Commands;

namespace Calculator.Tests
{
    [TestFixture]
    public class CommandTests
    {
        [TestCase("1 + 2", "3")]
        [TestCase("(1 + 2)", "3")]
        [TestCase("(1 + 2)*6 - (14 - 4) / 2 - 0.5", "12.5")]
        [TestCase("-(1 + 2)", "-3")]
        [TestCase("-1", "-1")]
        public void CalculateTest(string text, string expected)
        {
            Assert.True(decimal.TryParse(expected, NumberStyles.Float, CultureInfo.InvariantCulture, out var r));

            var tokens = text.GetCalculatorTokens();
            var ast = AstBuilder.BuildAst(tokens);
            var cmd = CmdBuilder.BuildCmdInvoker(ast);

            var result = cmd.Calculate(EmptyVariableValueProvider.Instance);

            Console.WriteLine($"Result: {result}");

            Assert.That(result, Is.EqualTo(r));
        }

        [Test]
        public void CalculateWithVarsTest1()
        {
            var tokens = "a + b/(2 + 7) - a*b".GetCalculatorTokens();
            var ast = AstBuilder.BuildAst(tokens);
            var cmd = CmdBuilder.BuildCmdInvoker(ast);

            foreach (var v in cmd.Variables)
            {
                Console.WriteLine(v);
            }

            var variables = new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 18,
            }.AsVariableValueProvider();

            var result = cmd.Calculate(variables);

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(-66m));
        }

        [Test]
        public void CalculateWithVarsTest2()
        {
            var tokens = "a + b/(2 + 7) - a*b".GetCalculatorTokens();
            var ast = AstBuilder.BuildAst(tokens);
            var cmd = CmdBuilder.BuildCmdInvoker(ast);

            var variables = new Dictionary<string, decimal>();
            foreach (var v in cmd.Variables)
            {
                var val = Random.Shared.Next() * 0.01m;
                Console.WriteLine($"{v}: {val}");
                variables[v] = val;
            }

            var result = cmd.Calculate(variables.AsVariableValueProvider());

            Console.WriteLine(result);

            var a = variables["a"];
            var b = variables["b"];

            Assert.That(result, Is.EqualTo(a + b / (2 + 7) - a * b));
        }
    }
}
