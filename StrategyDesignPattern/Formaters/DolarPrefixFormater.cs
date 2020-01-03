namespace StrategyDesignPattern.Formaters
{
	public class DolarPrefixFormater : IFormater
	{
		public string Format(decimal number, int precision)
			=> $"{number:C}";
	}
}
