using PriceCalculator.Models;
using System.Collections.Generic;

namespace PriceCalculator.Interfaces
{
	public interface IDiscountCalculationMode
	{
		Money CalculateDiscounts(IEnumerable<IDiscount> discounts, IProduct product);
	}
}
