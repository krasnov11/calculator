using System;
using System.Collections.Generic;
using System.Globalization;
using Calculations.Models.Scan;

namespace Calculator.Tests
{
    [TestFixture]
    public class CalculatorTest
    {
        [TestCase("1 + 2", "3")]
        [TestCase("(1 + 2)", "3")]
        [TestCase("(1 + 2)*6 - (14 - 4) / 2 - 0.5", "12.5")]
        [TestCase("-(1 + 2)", "-3")]
        [TestCase("-1", "-1")]
        public void CalculateTest(string text, string expected)
        {
            var scanner = new Scanner(text);

            while (scanner.Next())
            {
                Console.WriteLine($"{scanner.GetToken()}: '{scanner.GetTokenValue()}'");
            }

            Assert.False(scanner.HasErrors);

            Assert.True(decimal.TryParse(expected, NumberStyles.Float, CultureInfo.InvariantCulture, out var r));

            var calc = new Calculations.Calculator(text);
            var result = calc.Calculate();

            Console.WriteLine($"Result: {result}");

            Assert.That(result, Is.EqualTo(r));
        }

        [Test]
        public void CalculateWithVarsTest1()
        {
            var calc = new Calculations.Calculator("a + b");
            var result = calc.Calculate(new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 5,
            });

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(9m));
        }

        [Test]
        public void CalculateWithVarsTest2()
        {
            var calc = new Calculations.Calculator("a + b/2");
            var result = calc.Calculate(new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 5,
            });

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(6.5m));
        }

        [Test]
        public void CalculateWithVarsTest3()
        {
            var calc = new Calculations.Calculator("a + b/2 + 7");
            var result = calc.Calculate(new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 5,
            });

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(13.5m));
        }

        [Test]
        public void CalculateWithVarsTest4()
        {
            var calc = new Calculations.Calculator("a + b/(2 + 7)");
            var result = calc.Calculate(new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 18,
            });

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(6m));
        }

        [Test]
        public void CalculateWithVarsTest5()
        {
            var calc = new Calculations.Calculator("a + b/(2 + 7) - a*b");
            var result = calc.Calculate(new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 18,
            });

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(-66m));
        }

        [Test]
        public void CalculateWithVarsTest6()
        {
            var calc = new Calculations.Calculator("_a123_www + _b/(2 + 7) - _a123_www*_b");
            var result = calc.Calculate(new Dictionary<string, decimal>()
            {
                ["_a123_www"] = 4,
                ["_b"] = 18,
            });

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(-66m));
        }

        [Test]
        public void CalculateWithVarsTest7()
        {
            var calc = new Calculations.Calculator("a + -b/-(2 + 7) - a*b");
            var result = calc.Calculate(new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 18,
            });

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(-66m));
        }

        [Test]
        public void CalculateWithVarsTest8()
        {
            var calc = new Calculations.Calculator("a + b/-(2 + 7) - a * b");
            var result = calc.Calculate(new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 18,
            });

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(-70m));
        }

        [Test]
        public void CalculateWithVarsTest9()
        {
            var calc = new Calculations.Calculator("a + b/-(-2 + 7) - a * b");
            var result = calc.Calculate(new Dictionary<string, decimal>()
            {
                ["a"] = 4,
                ["b"] = 18,
            });

            Console.WriteLine(result);

            Assert.That(result, Is.EqualTo(-71.6m));
        }

        [TestCase("a + 4.1 / (3*n - 1)")]
        [TestCase("a + 42.12 / (3*n - 1)")]
        [TestCase("agg + 411 + (ngg - 111) 123")]
        [TestCase("- 1)")]
        [TestCase("+")]
        [TestCase("++")]
        [TestCase("++ )")]
        [TestCase("1)")]
        [TestCase("d)")]
        public void ScannerNextTest(string text)
        {
            var scanner = new Scanner(text);

            while (scanner.Next())
            {
                Console.WriteLine($"{scanner.GetToken()}: '{scanner.GetTokenValue()}'");
            }

            Assert.False(scanner.HasErrors);
        }
    }
}