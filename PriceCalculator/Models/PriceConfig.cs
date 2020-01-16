using PriceCalculator.PriceModifiers;

namespace PriceCalculator.Models
{
	public class PriceConfig
	{
		public TaxPriceModifier Tax { get; set; }
		public Discount[] Discounts { get; set; }
		public Expense[] AdditionalExpenses { get; set; }
		public string CurrencyFormat { get; set; }
		public string DiscountCalculationMode { get; set; }
		public decimal Cap { get; set; }
		public CostType CapType { get; set; }
	}
}
