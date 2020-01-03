using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using StrategyDesignPattern.PriceCalculationStrategies;

namespace StrategyDesignPattern.Strategies
{
	public class CalculationStrategy : IPriceCalculationStrategy
	{
		private readonly PriceModifiersBuilder priceModifiers;
		public IPriceCalculation PriceCalculation { get; set; }

		public CalculationStrategy(PriceModifiersBuilder priceModifiers)
		{
			this.priceModifiers = priceModifiers;
			PriceCalculation = priceModifiers.CalculationStrategy ?? new AdditivePriceCalculation();
		}

		public PriceCalculationResult CalculatePrice(IProduct product)
		{
			return PriceCalculation.GetPriceResultForProduct(product, priceModifiers);
		}
	}
}