using System.Globalization;
namespace StrategyDesignPattern.Formaters
{
	public class DolarSufixFormater : IFormater
	{

		public string Format(decimal number, int precision)
			=> $"{number} USD";

		public void Test(string name)
		{

		}
	}
}
