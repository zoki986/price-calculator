using StrategyDesignPattern.Common;
using StrategyDesignPattern.Formaters;
using StrategyDesignPattern.Interfaces;
using System;

namespace StrategyDesignPattern.PriceModifiers
{
	public class TaxPriceModifier : ITax
	{
		public IMoney Cost { get; set; }
		public int Precision { get; set; }

		public TaxPriceModifier(IMoney currency, int precision = 2)
		{
			Cost = currency;
			Precision = precision;
		}

		public IMoney ApllyPriceModifier(IProduct product)
		{
			var amount = (Cost.Ammount * product.Price.Ammount).WithPrecision(Precision);
			return (IMoney)Activator.CreateInstance(Cost.GetType(), amount);
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
