using PriceCalculator.Interfaces;
using PriceCalculator.PriceModifiersModels;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class DiscountExtensions
	{
		public static decimal ApplyPrecedence(this IEnumerable<IPrecedenceDiscount> discounts, IProduct product, IPriceModifierBuilder priceModifiers)
		{
			var precedenceDiscountSum = discounts
										.SumDiscounts(product)
										.WithDiscountCap(priceModifiers.DiscountCap, product);
			return product
				   .Price
				   .Substract(precedenceDiscountSum);
		}

		public static decimal ApplyPriceOperation(this IEnumerable<IProductTax> taxes, IProduct product)
			=> taxes.Select(x => x.ApllyPriceOperation(product)).Sum();

		public static decimal SumDiscounts(this IEnumerable<IDiscount> discounts, IProduct product) 
			=> discounts.Cast<Discount>().Aggregate(0M, (prev, next) => prev + next.ApllyPriceOperation(product));

		public static decimal SumDiscounts(this IEnumerable<IPrecedenceDiscount> discounts, IProduct product) 
			=> discounts.Cast<Discount>().Aggregate(0M, (prev, next) => prev + next.ApllyPriceOperation(product));

		public static decimal MultypliDiscounts(this IEnumerable<IDiscount> discounts, IProduct product)
			=> discounts.Aggregate(0M, (prev, next) => Calculate(prev, next, product.Price));
		private static decimal Calculate(decimal prev, IDiscount next, decimal productPrice)
			=> (((productPrice - prev) * next.Amount) + prev).WithPrecision(Constants.MoneyRelatedPrecision);

		public static decimal WithDiscountCalculationStrategy(this IEnumerable<IDiscount> discounts, IDiscountCalculationMode strategy, IProduct product)
			=> strategy.CalculateDiscounts(discounts, product);
	}
}
