using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.CalculationModes
{
	public class MultiplicativeDiscountCalculation : IDiscountCalculationMode
	{
		public Money CalculateDiscounts(IEnumerable<IDiscount> discounts, IProduct product)
			=> discounts.Aggregate(new Money(0M), (prev, next) => CalculateDiscount(prev, next, product.Price));
		private static Money CalculateDiscount(Money prev, IDiscount next, Money productPrice)
			=> (((productPrice - prev) * next.Amount) + prev).WithPrecision(Constants.MoneyRelatedPrecision);
	}
}
