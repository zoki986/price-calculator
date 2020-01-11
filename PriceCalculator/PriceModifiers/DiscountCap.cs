using PriceCalculator.Interfaces;
using System;
using ValueType = PriceCalculator.Models.ValueType;

namespace PriceCalculator.PriceModifiers
{
	public class DiscountCap
	{
		public decimal Cap { get; }
		public ValueType CapType { get; }

		public DiscountCap(decimal cap, ValueType type)
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
			if (CapType == ValueType.Monetary)
				return Cap;

			return product.Price * Cap;
		}
	}
}
