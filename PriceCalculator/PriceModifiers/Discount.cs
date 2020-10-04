using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiers
{
	public class Discount : IDiscount
	{
		public decimal Amount { get; set; }
		public int Precision { get;  set; } = 2;

		public Discount WithDiscount(decimal discount)
		{
			this.Amount = discount;
			return this;
		}

		public virtual decimal ApllyPriceOperation(IProduct product) 
			=> (product.Price * Amount).WithPrecision(Precision);
		public override string ToString() 
			=> $"Discount = {Amount}";
	}
}
