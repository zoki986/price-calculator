using PriceCalculator.Interfaces;

namespace PriceCalculator.Operations.Interfaces
{
	public interface ITaxHandler
	{
		decimal GetResult(IProduct product, IPriceModifierBuilder priceModifiers);
	}
}
