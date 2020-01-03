using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Common;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using System.Linq;

namespace StrategyDesignPattern.PriceCalculationStrategies
{
	public class MultiplicativeCalculation : IPriceCalculation
	{
		public PriceCalculationResult GetPriceResultForProduct(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			var additionlExpenses = priceModifiers.AdditionalExpenses.SumExpenses(product).Sum();

			var productWithNewPrice = priceModifiers.Discounts.ApplyPrecedence(product);

			var taxAmount = priceModifiers.Tax.ApllyPriceModifier(productWithNewPrice);

			var discountAmount = priceModifiers
								.Discounts
								.Where(discount => !discount.HasPrecedence)
								.MultypliDiscounts(productWithNewPrice)
								.WithDiscountCap(priceModifiers.Cap, product);

			var total = productWithNewPrice
						.Price
						.SumWith(taxAmount)
						.SumWith(additionlExpenses)
						.Substract(discountAmount)
						.WithPrecision(Constants.DefaultPrecision);

			return new PriceCalculationResult()
						.WithPriceBefore(product.Price)
						.WithProduct(product)
						.WithExpenses(priceModifiers.AdditionalExpenses)
						.WithTax(taxAmount)
						.WithDiscounts(discountAmount)
						.WithTotal(total);
		}
	}
}
