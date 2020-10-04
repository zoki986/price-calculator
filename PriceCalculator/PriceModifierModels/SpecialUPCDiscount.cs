using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiersModels
{
	public class SpecialUPCDiscount : Discount
	{
		public int UPC { get; set; }

		new public SpecialUPCDiscount WithDiscount(decimal discount)
		{
			this.Amount = discount;
			return this;
		}

		public SpecialUPCDiscount WithUPC(int upc)
		{
			UPC = upc;
			return this;
		}

		public override decimal ApllyPriceOperation(IProduct product)
		{
			if (product.UPC != UPC)
				return 0;

			return (Amount * product.Price).WithPrecision(Precision);
		}
	}
}
