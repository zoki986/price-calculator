using PriceCalculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class DiscountExtensions
	{
		public static IProduct ApplyPrecedence(this IEnumerable<IDiscount> discounts, IProduct product, IPriceModifierBuilder priceModifiers)
		{
			var newPriceAmount = product
								.Price
								.Substract(
									discounts.Where(discount => discount.HasPrecedence)
									.SumDiscounts(product)
									.WithDiscountCap(priceModifiers.DiscountCap, product)
									);

			return (IProduct)Activator.CreateInstance(product.GetType(), product.Name, product.UPC, newPriceAmount);
		}
		public static decimal SumDiscounts(this IEnumerable<IDiscount> discounts, IProduct product)
		{
			return discounts.Aggregate(0M, (prev, next) => prev + next.ApllyPriceModifier(product));
		}

		public static decimal MultypliDiscounts(this IEnumerable<IDiscount> discounts, IProduct product)
		{
			return discounts.Aggregate(0M, (prev, next) => Calculate(prev, next, product.Price));
		}
		private static decimal Calculate(decimal prev, IDiscount next, decimal productPrice)
		{
			return (((productPrice - prev) * next.DiscountAmount) + prev).WithPrecision(Constants.MoneyRelatedPrecision);
		}

		public static decimal WithDiscountCalculationStrategy(this IEnumerable<IDiscount> discounts, Func<IEnumerable<IDiscount>, IProduct, decimal> strategy, IProduct product)
		{
			return strategy(discounts, product);
		}
	}
}
