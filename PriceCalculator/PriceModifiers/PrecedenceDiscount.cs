using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiers
{
	public class PrecedenceDiscount :  SpecialUPCDiscount, IPrecedenceDiscount
	{
		new public PrecedenceDiscount WithDiscount(decimal discount)
		{
			this.Amount = discount;
			return this;
		}

		new public PrecedenceDiscount WithUPC(int upc)
		{
			UPC = upc;
			return this;
		}
	}

	public interface IPrecedenceDiscount
	{
	}
}
