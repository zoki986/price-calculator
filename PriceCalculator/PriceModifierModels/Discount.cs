using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.PriceModifiersModels
{
	public class Discount : IDiscount
	{
		public decimal Amount { get; set; }
		public int Precision { get;  set; } = 2;

		public Discount WithDiscount(decimal discount)
		{
			Amount = discount;
			return this;
		}

		public virtual decimal ApllyModifier(IProduct product) 
			=> (product.Price.Amount * Amount).WithPrecision(Precision);
		public override string ToString() 
			=> $"Discount = {Amount}";
	}
}
