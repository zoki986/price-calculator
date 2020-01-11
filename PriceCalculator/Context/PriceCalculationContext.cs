using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.PriceCalculationStrategies;
using PriceCalculator.Reporters;
using System;

namespace PriceCalculator.Context
{
	public class PriceCalculationContext
	{
		private IPriceCalculation _calculator = new SimplePriceCalculator();
		private PriceModifiersBuilder _priceModifiers;
		public PriceCalculationContext() { }

		public PriceCalculationContext(PriceModifiersBuilder priceModifiers)
		{
			_priceModifiers = priceModifiers ?? throw new ArgumentNullException(nameof(priceModifiers));
		}

		public void SetModifiers(PriceModifiersBuilder priceModifiers)
		{
			_priceModifiers = priceModifiers ?? throw new ArgumentNullException(nameof(priceModifiers));
		}

		public void CalculateAndReportPrice(IProduct product)
		{
			ConsoleReporter.PrintResult(_calculator.GetPriceResultForProduct(product, _priceModifiers));
		}
	}
}
