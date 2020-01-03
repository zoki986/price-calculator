using StrategyDesignPattern.Common;
using StrategyDesignPattern.Formaters;
using StrategyDesignPattern.Interfaces;
using System;

namespace StrategyDesignPattern.PriceModifiers
{
	public class Discount : IDiscount
	{
		public IMoney Amount { get; set; }
		public IFormater Formater { get; }
		public int Precision { get;  set; } = 2;
		public bool HasPrecedence { get; set; }

		public Discount WithDiscount(IMoney discount)
		{
			this.Amount = discount;
			return this;
		}

		public Discount WithPrecedence()
		{
			HasPrecedence = true;
			return this;
		}

		public Discount WithPrecision(int precision)
		{
			this.Precision = precision;
			return this;
		}

		public IMoney ApllyPriceModifier(IProduct product)
		{
			var amount = (product.Price.Ammount * Amount.Ammount).WithPrecision(Precision);
			return (IMoney)Activator.CreateInstance(Amount.GetType(), amount);
		}
		public override string ToString()
		{
			return $"Discount = {Amount.ToString()}";
		}
	}
}
