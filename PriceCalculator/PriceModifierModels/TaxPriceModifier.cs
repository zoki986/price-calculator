using PriceCalculator.Common;
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

		public decimal ApllyModifier(IProduct product) 
			=> (product.Price * Amount).WithPrecision(Precision).Amount;

		public override string ToString() 
			=> $"Tax = {Amount}";
	}
}
