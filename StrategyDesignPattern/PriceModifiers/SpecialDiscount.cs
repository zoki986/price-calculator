using StrategyDesignPattern.Common;
using StrategyDesignPattern.Formaters;
using StrategyDesignPattern.Interfaces;
using System;

namespace StrategyDesignPattern.PriceModifiers
{
	public class SpecialDiscount : Discount
	{
		public int UPC { get; set; }

		new public SpecialDiscount WithDiscount(IMoney discount)
		{
			this.Amount = discount;
			return this;
		}

		 public SpecialDiscount WithUPC(int upc)
		{
			UPC = upc;
			return this;
		}

		new public SpecialDiscount WithPrecedence()
		{
			HasPrecedence = true;
			return this;
		}

		new public SpecialDiscount WithPrecision(int precision)
		{
			Precision = precision;
			return this;
		}
		new public IMoney ApllyPriceModifier(IProduct product)
		{
			if (product.UPC != UPC)
				return product.Price;

			var amount = (Amount.Ammount * product.Price.Ammount).WithPrecision(Precision);
			return (IMoney)Activator.CreateInstance(Amount.GetType(), amount, Formater);
		}
	}
}
