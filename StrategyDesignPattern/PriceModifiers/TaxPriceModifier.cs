using StrategyDesignPattern.Common;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using System;

namespace StrategyDesignPattern.PriceModifiers
{
	public class TaxPriceModifier : ITax
	{
		public Money Cost { get; set; }
		public int Precision { get; set; }

		public TaxPriceModifier(Money amount, int precision = 2)
		{
			Cost = amount;
			Precision = precision;
		}

		public Money ApllyPriceModifier(IProduct product)
		{
			var amount = (Cost.Amount * product.Price.Amount).WithPrecision(Precision);
			return new Money(amount);
		}

		public TaxPriceModifier WithPrecision(int precision)
		{
			Precision = precision;
			return this;
		}

		public override string ToString()
		{
			return $"Tax = {Cost.ToString()}";
		}
	}
}
