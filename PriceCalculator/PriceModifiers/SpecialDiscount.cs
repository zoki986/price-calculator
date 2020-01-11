using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiers
{
	public class SpecialDiscount : Discount
	{

		new public SpecialDiscount WithDiscount(decimal discount)
		{
			this.DiscountAmount = discount;
			return this;
		}

		public SpecialDiscount WithUPC(int upc)
		{
			UPC = upc;
			return this;
		}

		new public SpecialDiscount WithPrecedence()
		{
			HasPrecedence = true;
			return this;
		}

		new public SpecialDiscount WithPrecision(int precision)
		{
			Precision = precision;
			return this;
		}
		new public decimal ApllyPriceModifier(IProduct product)
		{
			if (product.UPC != UPC)
				return 0;

			var amount = (DiscountAmount * product.Price).WithPrecision(Precision);
			return amount;
		}
	}
}
