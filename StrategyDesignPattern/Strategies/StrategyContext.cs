using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Reporters;
using System;

namespace StrategyDesignPattern.Strategies
{
	public class StrategyContext
	{
		private IPriceCalculationStrategy _strategy;
		public IPriceCalculation PriceCalculation { get; set; }

		public StrategyContext() { }

		public StrategyContext(IPriceCalculationStrategy strategy)
			=> _strategy = strategy;

		public void SetStrategy(IPriceCalculationStrategy strategy)
			=> _strategy = strategy;

		public void CalculatePrice(IProduct product)
		{
			var result = this._strategy.CalculatePrice(product);
			ConsoleReporter.PrintResult(result);
		}
	}
}
