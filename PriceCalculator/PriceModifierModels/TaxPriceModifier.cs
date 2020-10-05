using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiersModels
{
	public class TaxPriceModifier : IProductTax
	{
		public decimal Amount { get; set; }
		public TaxPriceModifier(decimal amount)
		{
			Amount = amount;
		}

		public decimal ApllyModifier(IProduct product) 
			=> (product.Price * Amount).Amount;

		public override string ToString() 
			=> $"Tax = {Amount}";
	}
}
