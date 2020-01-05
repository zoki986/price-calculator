using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System;

namespace PriceCalculator.PriceModifiers
{
	public class SpecialDiscount : Discount
	{

		new public SpecialDiscount WithDiscount(Money discount)
		{
			this.DiscountAmount = discount;
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
				return (IMoney)Activator.CreateInstance(DiscountAmount.GetType(), 0);

			var amount = (DiscountAmount.Amount * product.Price.Amount).WithPrecision(Precision);
			return (IMoney)Activator.CreateInstance(DiscountAmount.GetType(), amount);
		}
	}
}
