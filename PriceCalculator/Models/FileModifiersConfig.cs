using PriceCalculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculator.Models
{
	public class FileModifiersConfig
	{
		public List<IPriceModifier> PriceModifiers { get; set; } = new List<IPriceModifier>();
		public NumberFormatInfo CurrencyFormat { get; set; }
		public IDiscountCalculationMode DiscountCalculationMode { get; set; }
		public decimal Cap { get; set; }
		public IExpenseType CapType { get; set; }
	}
}
