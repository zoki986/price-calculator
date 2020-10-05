using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System.Linq;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class CalculationStrategy : IPriceCalculation
	{
		public PriceCalculationResult GetPriceResultForProduct(IProduct product, IProductModifiersBuilder priceModifiers) 
			=> BuildPriceCalculationResult(product, priceModifiers, CalculateProductCosts(product, priceModifiers));

		private ProductCostsReport CalculateProductCosts(IProduct product, IProductModifiersBuilder priceModifiers) 
			=> product
				.CalculatePrecedenceDiscount(priceModifiers)
				.CalculateTax(priceModifiers)
				.CalculateDiscounts(product, priceModifiers)
				.CalculateExpenses(product, priceModifiers)
				.CalculateTotal(product, priceModifiers);

		PriceCalculationResult BuildPriceCalculationResult(IProduct product, IProductModifiersBuilder priceModifiers, ProductCostsReport costs)
			=> new PriceCalculationResult()
			   .ForProduct(product)
			   .WithInitialPrice(product.Price)
			   .WithExpenses(priceModifiers.ProductPriceModifiers.OfType<IExpense>())
			   .WithTax(costs.Tax)
			   .WithDiscounts(costs.Discounts)
			   .WithTotal(costs.Total)
			   .WithFormat(priceModifiers.CurrencyFormat);
	}
}
