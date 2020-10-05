using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class SumingDiscountCalculation : IDiscountCalculationMode
	{
		public Money CalculateDiscounts(IEnumerable<IDiscount> discounts, IProduct product)
			=> discounts.Aggregate(new Money(0M), (prev, next) => prev + next.ApllyModifier(product));
	}
}
