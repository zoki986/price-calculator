using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System;

namespace PriceCalculator.PriceModifiers
{
	public class TaxPriceModifier : ITax
	{
		public decimal Cost { get; set; }
		public int Precision { get; set; }

		public TaxPriceModifier(decimal amount, int precision = 2)
		{
			Cost = amount;
			Precision = precision;
		}

		public decimal ApllyPriceModifier(IProduct product)
		{
			return (Cost* product.Price).WithPrecision(Precision);
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
