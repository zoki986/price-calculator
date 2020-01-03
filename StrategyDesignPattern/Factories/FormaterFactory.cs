using StrategyDesignPattern.Formaters;

namespace StrategyDesignPattern.Factories
{
	public class FormaterFactory : IFormaterAbstractFactory
	{
		public IFormater CreateDolarPrefixFormater()
			=> new DolarPrefixFormater();

		public IFormater CreateDolarSufixFormater()
			=> new DolarSufixFormater();
	}
}
