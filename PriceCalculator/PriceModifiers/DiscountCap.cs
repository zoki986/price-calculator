using PriceCalculator.Interfaces;
using System;
using CostType = PriceCalculator.Models.CostType;

namespace PriceCalculator.PriceModifiers
{
	public class DiscountCap
	{
		public decimal Cap { get; }
		public CostType CapType { get; }

		public DiscountCap(decimal cap, CostType type)
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
		{
			if (CapType == CostType.Monetary)
				return Cap;

			return product.Price * Cap;
		}
	}
}
