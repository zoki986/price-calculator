using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.PriceCalculationStrategies;
using StrategyDesignPattern.Reporters;
using System;

namespace StrategyDesignPattern.Strategies
{
	public class PriceCalculationContext
	{
		private IPriceCalculation _strategy;
		private PriceModifiersBuilder _priceModifiers;
		public PriceCalculationContext() { }

		public PriceCalculationContext(PriceModifiersBuilder priceModifiers)
		{
			_priceModifiers = priceModifiers ?? throw new ArgumentNullException(nameof(priceModifiers));
			_strategy = priceModifiers.CalculationStrategy ?? new AdditivePriceCalculation();
		}

		public void SetStrategy(IPriceCalculation strategy)
			=> _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

		public void SetModifiers(PriceModifiersBuilder priceModifiers)
		{
			_priceModifiers = priceModifiers ?? throw new ArgumentNullException(nameof(priceModifiers));
			_strategy = priceModifiers.CalculationStrategy;
		}

		public void CalculateAndReportPrice(IProduct product)
		{
			ConsoleReporter.PrintResult(_strategy.GetPriceResultForProduct(product, _priceModifiers));
		}
	}
}
