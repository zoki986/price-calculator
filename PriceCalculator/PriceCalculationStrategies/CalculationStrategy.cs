using PriceCalculator.Builder;
using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiers;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class CalculationStrategy : IPriceCalculation
	{
		public PriceCalculationResult GetPriceResultForProduct(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			ProductCosts costs = CalculateProductCosts(product, priceModifiers);

			return BuildPriceCalculationResult(product, priceModifiers, costs);
		}

		private ProductCosts CalculateProductCosts(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			return product
				.ApplyPrecedenceDiscount(priceModifiers)
				.ApplyTax(priceModifiers)
				.ApplyDiscounts(product, priceModifiers)
				.ApplyExpenses(product, priceModifiers)
				.CalculateTotal(product, priceModifiers);
		}

		PriceCalculationResult BuildPriceCalculationResult(IProduct product, IPriceModifierBuilder priceModifiers, ProductCosts costs)
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
