using StrategyDesignPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyDesignPattern.Common
{
	public static class DiscountExtensions
	{
		public static IProduct ApplyPrecedence(this IEnumerable<IDiscount> discounts, IProduct product)
		{
			var newPriceAmount = product
								.Price
								.Substract(discounts.Where(discount => discount.HasPrecedence).SumDiscounts(product));

			var newPrice = (IMoney)Activator.CreateInstance(product.Price.GetType(), newPriceAmount);

			return (IProduct)Activator.CreateInstance(product.GetType(), product.Name, product.UPC, newPrice);
		}
		public static IMoney SumDiscounts(this IEnumerable<IDiscount> discounts, IProduct product)
		{
			var discountsSumed = discounts.Aggregate(0M, (prev, next) => prev + next.ApllyPriceModifier(product).Ammount);
			return (IMoney)Activator.CreateInstance(product.Price.GetType(), discountsSumed);
		}

		public static IMoney MultypliDiscounts(this IEnumerable<IDiscount> discounts, IProduct product)
		{
			var productPrice = product.Price.Ammount;
			var discountsMultiplied = discounts.Aggregate(0M, (prev, next) => Calculate(prev, next, productPrice));
			return (IMoney)Activator.CreateInstance(product.Price.GetType(), discountsMultiplied);
		}
		private static decimal Calculate(decimal prev, IDiscount next, decimal productPrice)
		{
			return (((productPrice - prev) * next.Amount.Ammount) + prev).WithPrecision(Constants.MoneyRelatedPrecision);
		}
	}
}
