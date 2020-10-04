using System.Collections.Generic;

namespace PriceCalculator.Interfaces
{
	public interface IDiscountCalculationMode
	{
		decimal CalculateDiscounts(IEnumerable<IDiscount> discounts, IProduct product);
	}
}
