namespace Calculations.Models.Scan
{
    /// <summary>
    /// Токены разбора строки калькуляторв
    /// </summary>
    public enum Tokens
    {
        /// <summary>
        /// Число (Пример: 12 или 1.33)
        /// </summary>
        Number,

        /// <summary>
        /// Переменная. Начало - букава или подчеркивание, далее - букава или подчеркивание или цифра
        /// </summary>
        Variable,

        /// <summary>
        /// Левая скобка
        /// </summary>
        LeftBracket,

        /// <summary>
        /// Правая скобка
        /// </summary>
        RightBracket,

        /// <summary>
        /// Оператор. + - / *
        /// </summary>
        Operator
    }
}
