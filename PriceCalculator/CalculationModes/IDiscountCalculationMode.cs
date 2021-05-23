using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System.Collections.Generic;

namespace PriceCalculator.CalculationModes
{
	public interface IDiscountCalculationMode
	{
		Money CalculateDiscounts(IEnumerable<IDiscount> discounts, IProduct product);
	}
}
