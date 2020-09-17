using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.Operations.Interfaces;

namespace PriceCalculator.Operations.Implementations
{
	public class PriceResultHandler : IPriceResultHandler
	{
		public PriceCalculationResult GetResult(IProduct product, IPriceModifierBuilder priceModifiers, ProductCosts costs)
		{
			return new PriceCalculationResult()
			   .ForProduct(product)
			   .WithInitialPrice(product.Price)
			   .WithExpenses(priceModifiers.AdditionalExpenses)
			   .WithTax(costs.Tax)
			   .WithDiscounts(costs.Discounts)
			   .WithTotal(costs.Total)
			   .WithFormat(priceModifiers.CurrencyFormat);
		}
	}
}
