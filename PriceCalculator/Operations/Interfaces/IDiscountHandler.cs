using PriceCalculator.Interfaces;

namespace PriceCalculator.Operations.Interfaces
{
	public interface IDiscountHandler
	{
		decimal GetResult(IProduct product, IPriceModifierBuilder priceModifiers, IProduct precedenceDiscountProduct);
	}
}
