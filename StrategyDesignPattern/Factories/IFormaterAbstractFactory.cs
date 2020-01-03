using StrategyDesignPattern.Formaters;

namespace StrategyDesignPattern.Factories
{
	public interface IFormaterAbstractFactory
	{
		IFormater CreateDolarPrefixFormater();
		IFormater CreateDolarSufixFormater();
	}
}
