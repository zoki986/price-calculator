using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class CalculationStrategy : IPriceCalculation
	{
		public PriceCalculationResult GetPriceResultForProduct(IProduct product, IProductModifiersBuilder priceModifiers)
		{
			var productCosts = CalculateProductCosts(product, priceModifiers);

			return new PriceCalculationResult(product, priceModifiers, productCosts);
		}

		private ProductCostsReport CalculateProductCosts(IProduct product, IProductModifiersBuilder priceModifiers) 
			=> product
				.CalculatePrecedenceDiscount(priceModifiers)
				.CalculateTax(priceModifiers)
				.CalculateDiscounts(product, priceModifiers)
				.CalculateExpenses(product, priceModifiers)
				.CalculateTotal(product, priceModifiers);
	}
}
