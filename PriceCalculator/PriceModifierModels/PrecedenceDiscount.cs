namespace PriceCalculator.PriceModifiersModels
{
	public interface IPrecedenceDiscount {}
	public class PrecedenceDiscount :  SpecialUPCDiscount, IPrecedenceDiscount
	{
		public new PrecedenceDiscount WithDiscount(decimal discount)
		{
			Amount = discount;
			return this;
		}

		public new PrecedenceDiscount WithUPC(int upc)
		{
			UPC = upc;
			return this;
		}
	}
}
