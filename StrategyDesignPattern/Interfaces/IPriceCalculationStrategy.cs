using StrategyDesignPattern.Models;

namespace StrategyDesignPattern.Interfaces
{
	public interface IPriceCalculationStrategy
	{
		PriceCalculationResult CalculatePrice(IProduct product);
	}
}
