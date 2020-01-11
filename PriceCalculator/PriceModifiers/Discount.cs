using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System;

namespace PriceCalculator.PriceModifiers
{
	public class Discount : IDiscount
	{
		public decimal DiscountAmount { get; set; }
		public int Precision { get;  set; } = 2;
		public bool HasPrecedence { get; set; }
		public int UPC { get; set; }

		public Discount WithDiscount(decimal discount)
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

		public virtual decimal ApllyPriceModifier(IProduct product)
		{
			return (product.Price * DiscountAmount).WithPrecision(Precision);
		}
		public override string ToString()
		{
			return $"Discount = {DiscountAmount.ToString()}";
		}
	}
}
