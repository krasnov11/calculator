namespace Calculations.Models.Commands
{
    internal class CmdConstant : CmdBase
    {
        private readonly decimal _constant;

        public CmdConstant(decimal constant)
        {
            _constant = constant;
        }

        public override decimal Calculate()
        {
            return _constant;
        }
    }
}
