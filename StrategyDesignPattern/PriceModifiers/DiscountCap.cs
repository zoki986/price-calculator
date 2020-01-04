using StrategyDesignPattern.Interfaces;
using System;
using ValueType = StrategyDesignPattern.Models.ValueType;

namespace StrategyDesignPattern.PriceModifiers
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
		public IMoney GetMaxDiscount(IMoney money, IProduct product)
		{
			var maxCap = GetMaxCap(product);
			return maxCap <= money.Amount ? (IMoney)Activator.CreateInstance(money.GetType(), maxCap) : money;
		}
		private decimal GetMaxCap(IProduct product)
		{
			if (CapType == ValueType.Monetary)
				return Cap;

			return product.Price.Amount * Cap;
		}
	}
}
