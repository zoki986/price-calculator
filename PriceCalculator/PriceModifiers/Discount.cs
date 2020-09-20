using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiers
{
	public class Discount : IDiscount
	{
		public decimal DiscountAmount { get; set; }
		public int Precision { get;  set; } = 2;

		public Discount WithDiscount(decimal discount)
		{
			this.DiscountAmount = discount;
			return this;
		}

		public Discount WithPrecision(int precision)
		{
			this.Precision = precision;
			return this;
		}

		public virtual decimal ApllyPriceOperation(IProduct product) 
			=> (product.Price * DiscountAmount).WithPrecision(Precision);
		public override string ToString() 
			=> $"Discount = {DiscountAmount}";
	}
}
