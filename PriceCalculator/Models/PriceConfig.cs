using PriceCalculator.Interfaces;
using System.Collections.Generic;

namespace PriceCalculator.Models
{
	public class PriceConfig
	{
		public List<IPriceModifier> PriceModifiers { get; set; } = new List<IPriceModifier>();
		public string CurrencyFormat { get; set; }
		public IDiscountCalculationMode DiscountCalculationMode { get; set; }
		public decimal Cap { get; set; }
		public IExpenseType CapType { get; set; }
	}
}
