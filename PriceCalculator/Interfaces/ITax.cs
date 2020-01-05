using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface ITax
	{
		Money Cost { get; }
		int Precision { get; }
		Money ApllyPriceModifier(IProduct product);
	}
}
