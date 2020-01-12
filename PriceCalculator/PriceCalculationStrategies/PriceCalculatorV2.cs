using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.Operations.Implementations;
using PriceCalculator.Operations.Interfaces;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class PriceCalculatorV2 : IPriceCalculation
	{
		IPrecedenceHandler PrecedenceHandler { get; set; } = new PrecedenceHandler();
		ICostsCalculationHandler CostsCalculationHandler { get; set; } = new CostsCalculatorHandler();
		IPriceResultHandler PriceResultHandler { get; set; } = new PriceResultHandler();

		public PriceCalculationResult GetPriceResultForProduct(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			var productWithPrecedenceDiscount = PrecedenceHandler.GetResult(product, priceModifiers);

			Costs costs = CostsCalculationHandler.GetCosts(product, priceModifiers, productWithPrecedenceDiscount);

			return PriceResultHandler.GetResult(product, priceModifiers, costs);
		}

		public PriceCalculatorV2 WithPrecedenceHandler(IPrecedenceHandler handler)
		{
			PrecedenceHandler = handler;
			return this;
		}
		public PriceCalculatorV2 WithCostCalcutaionHandler(ICostsCalculationHandler handler)
		{
			CostsCalculationHandler = handler;
			return this;
		}
		public PriceCalculatorV2 WithPriceResultHandler(IPriceResultHandler handler)
		{
			PriceResultHandler = handler;
			return this;
		}
	}
}
