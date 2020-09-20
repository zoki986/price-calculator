using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.PriceModifiers
{
	public class DiscountCap
	{
		public decimal Cap { get; }
		public ICostType CapType { get; }

		public DiscountCap(decimal cap, ICostType type)
		{
			CapType = type;
			Cap = cap;
		}
		public decimal GetMaxDiscount(decimal discount, IProduct product)
		{
			var discountCap = GetDiscountCap(product);
			return discount >= discountCap ? discountCap : discount;
		}
		private decimal GetDiscountCap(IProduct product)
			=> CapType.GetCostAmount(Cap, product.Price);
	}
}
