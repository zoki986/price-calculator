﻿using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiersModels
{
	public class TaxPriceModifier : IProductTax
	{
		public decimal Amount { get; set; }
		public int Precision { get; set; }

		public TaxPriceModifier(decimal amount, int precision = 2)
		{
			Amount = amount;
			Precision = precision;
		}

		public decimal ApllyPriceOperation(IProduct product)
		{
			return (Amount* product.Price).WithPrecision(Precision);
		}

		public override string ToString()
		{
			return $"Tax = {Amount}";
		}
	}
}