using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Models;

namespace StrategyDesignPattern.Interfaces
{
	public interface IPriceCalculation
	{
		PriceCalculationResult GetPriceResultForProduct(IProduct product, PriceModifiersBuilder priceModifiers);
	}
}
