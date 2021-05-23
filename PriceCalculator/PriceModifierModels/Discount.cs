using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiersModels
{
	public class Discount : IDiscount
	{
		public decimal Amount { get; set; }

		public Discount WithDiscount(decimal discount)
		{
			Amount = discount;
			return this;
		}

		public virtual decimal ApllyModifier(IProduct product)
			=> product.Price.Amount * Amount;
		public override string ToString() 
			=> $"Discount = {Amount}";
	}
}
