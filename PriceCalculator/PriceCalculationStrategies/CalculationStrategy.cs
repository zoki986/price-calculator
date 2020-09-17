using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.Operations.Implementations;
using PriceCalculator.Operations.Interfaces;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class CalculationStrategy : IPriceCalculation
	{
		IPrecedenceHandler PrecedenceHandler { get; set; } = new PrecedenceHandler();
		ICostsCalculationHandler CostsCalculationHandler { get; set; } = new CostsCalculatorHandler();
		IPriceResultHandler PriceResultHandler { get; set; } = new PriceResultHandler();

		public PriceCalculationResult GetPriceResultForProduct(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			var productWithPrecedenceDiscount = PrecedenceHandler.GetResult(product, priceModifiers);

			ProductCosts costs = CostsCalculationHandler.GetProductCosts(product, priceModifiers, productWithPrecedenceDiscount);

			return PriceResultHandler.GetResult(product, priceModifiers, costs);
		}
	}
}
