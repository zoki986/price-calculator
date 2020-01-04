using StrategyDesignPattern.Common;
using StrategyDesignPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyDesignPattern.PriceCalculationStrategies
{
	public class PriceCalculationFunctions
	{
		public static IMoney SumDiscounts(IEnumerable<IDiscount> discounts, IProduct product)
		{
			var discountsSumed = discounts.Aggregate(0M, (prev, next) => prev + next.ApllyPriceModifier(product).Amount);
			return (IMoney)Activator.CreateInstance(product.Price.GetType(), discountsSumed);
		}
		public static IMoney MultypliDiscounts(IEnumerable<IDiscount> discounts, IProduct product)
		{
			var productPrice = product.Price.Amount;
			var discountsMultiplied = discounts.Aggregate(0M, (prev, next) => Calculate(prev, next, productPrice));
			return (IMoney)Activator.CreateInstance(product.Price.GetType(), discountsMultiplied);
		}
		private static decimal Calculate(decimal prev, IDiscount next, decimal productPrice)
		{
			return (((productPrice - prev) * next.DiscountAmount.Amount) + prev).WithPrecision(Constants.MoneyRelatedPrecision);
		}
	}
}
