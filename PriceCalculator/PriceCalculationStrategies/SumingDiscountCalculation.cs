using PriceCalculator.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class SumingDiscountCalculation : IDiscountCalculationMode
	{
		public decimal CalculateDiscounts(IEnumerable<IDiscount> discounts, IProduct product)
			=> discounts.Aggregate(0M, (prev, next) => prev + next.ApllyPriceOperation(product));
	}
}
