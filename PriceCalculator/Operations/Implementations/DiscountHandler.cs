using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Operations.Interfaces;
using System.Linq;

namespace PriceCalculator.Operations.Implementations
{
	public class DiscountHandler : IDiscountHandler
	{
		public decimal GetResult(IProduct product, IPriceModifierBuilder pm, IProduct precedenceDiscountProduct)
		{
			return  pm
					.Discounts
					.Where(discount => !discount.HasPrecedence)
					.WithDiscountCalculationStrategy(pm.DiscountCalculationMode, precedenceDiscountProduct)
					.WithDiscountCap(pm.DiscountCap, product);
		}
	}
}
