using System.Text;

namespace Calculations
{
    /// <summary>
    /// Класс получения токенов для разбора выражения
    /// </summary>
    public class Scanner
    {
        private readonly string _text;
        private Tokens _token;
        private string _tokenValue;
        private int _charPos;
        private bool _error;
        private readonly StringBuilder _sb = new StringBuilder();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text"></param>
        public Scanner(string text)
        {
            _text = text;
            _charPos = -1;
        }

        /// <summary>
        /// Получить текущий токен
        /// </summary>
        /// <returns></returns>
        public Tokens GetToken() => _token;

        /// <summary>
        /// Получить значение текущего токена
        /// </summary>
        /// <returns></returns>
        public string GetTokenValue() => _tokenValue;

        /// <summary>
        /// Есть ли ошибки разбора
        /// </summary>
        public bool HasErrors => _error;

        private string GetSequence(Predicate<char> pr)
        {
            _sb.Clear();
            while (_charPos < _text.Length && pr(_text[_charPos]))
            {
                _sb.Append(_text[_charPos]);
                _charPos++;
            }

            if (_charPos < _text.Length)
                --_charPos;

            var result = _sb.ToString();
            _sb.Clear();

            return result;
        }

        /// <summary>
        /// Перейти к следующему токену
        /// </summary>
        /// <returns></returns>
        public bool Next()
        {
            if (_error)
                return false;

            SkipSpace();

            if (_charPos >= _text.Length)
                return false;

            if (char.IsNumber(_text[_charPos]))
            {
                _token = Tokens.Number;
                _tokenValue = GetSequence(char.IsNumber);

                var nextChar = _charPos + 1;
                if (nextChar < _text.Length && _text[nextChar] == '.')
                {
                    _charPos += 2;

                    var d = GetSequence(char.IsNumber);

                    if (string.IsNullOrEmpty(d))
                    {
                        _error = true;
                        Console.WriteLine("Incorrect number");
                        return false;
                    }

                    _tokenValue += $".{d}";
                }
            }
            else if (char.IsLetter(_text[_charPos]) || _text[_charPos] == '_')
            {
                _token = Tokens.Variable;

                // тут первая будет '_' или буква, тк первая проверка уже была сверху
                _tokenValue = GetSequence(c => char.IsLetterOrDigit(c) || c == '_');
            }
            else
            {
                switch (_text[_charPos])
                {
                    case '(': _token = Tokens.LeftBracket; break;
                    case ')': _token = Tokens.RightBracket; break;
                    case '+':
                    case '*':
                    case '/':
                    case '-': _token = Tokens.Operator; break;

                    default:
                        _error = true;
                        Console.WriteLine($"Unexpected char '{_text[_charPos]}' (pos {_charPos})");
                        return false;
                }
                _tokenValue = _text[_charPos].ToString();
            }

            return true;
        }

        private void SkipSpace()
        {
            _charPos++;
            while (_charPos < _text.Length && char.IsWhiteSpace(_text[_charPos]))
            {
                _charPos++;
            }
        }
    }
}
