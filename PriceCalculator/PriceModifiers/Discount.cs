using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System;

namespace PriceCalculator.PriceModifiers
{
	public class Discount : IDiscount
	{
		public Money DiscountAmount { get; set; }
		public int Precision { get;  set; } = 2;
		public bool HasPrecedence { get; set; }
		public int UPC { get; set; }

		public Discount WithDiscount(Money discount)
		{
			this.DiscountAmount = discount;
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
			var amount = (product.Price.Amount * DiscountAmount.Amount).WithPrecision(Precision);
			return (IMoney)Activator.CreateInstance(DiscountAmount.GetType(), amount);
		}
		public override string ToString()
		{
			return $"Discount = {DiscountAmount.ToString()}";
		}
	}
}
