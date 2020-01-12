using PriceCalculator.Interfaces;

namespace PriceCalculator.Operations.Interfaces
{
	public interface IPrecedenceHandler
	{
		IProduct GetResult(IProduct product, IPriceModifierBuilder priceModifiers);
	}
}
