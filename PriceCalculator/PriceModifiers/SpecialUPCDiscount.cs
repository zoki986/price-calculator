using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiers
{
	public class SpecialUPCDiscount : Discount
	{
		public int UPC { get; set; }

		new public SpecialUPCDiscount WithDiscount(decimal discount)
		{
			this.DiscountAmount = discount;
			return this;
		}

		public SpecialUPCDiscount WithUPC(int upc)
		{
			UPC = upc;
			return this;
		}

		new public SpecialUPCDiscount WithPrecision(int precision)
		{
			Precision = precision;
			return this;
		}
		public override decimal ApllyPriceOperation(IProduct product)
		{
			if (product.UPC != UPC)
				return 0;

			return (DiscountAmount * product.Price).WithPrecision(Precision);
		}
	}
}
