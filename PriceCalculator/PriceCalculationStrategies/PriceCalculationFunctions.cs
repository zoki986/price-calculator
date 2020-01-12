using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class PriceCalculationFunctions
	{
		public static decimal SumDiscounts(IEnumerable<IDiscount> discounts, IProduct product)
		{
			return discounts.Aggregate(0M, (prev, next) => prev + next.ApllyPriceModifier(product));
		}
		public static decimal MultypliDiscounts(IEnumerable<IDiscount> discounts, IProduct product)
		{
			return discounts.Aggregate(0M, (prev, next) => CalculateDiscount(prev, next, product.Price));
		}
		private static decimal CalculateDiscount(decimal prev, IDiscount next, decimal productPrice)
		{
			return (((productPrice - prev) * next.DiscountAmount) + prev).WithPrecision(Constants.MoneyRelatedPrecision);
		}
	}
}
