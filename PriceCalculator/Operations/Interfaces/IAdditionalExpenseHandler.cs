using PriceCalculator.Interfaces;

namespace PriceCalculator.Operations.Interfaces
{
	public interface IAdditionalExpenseHandler 
	{
		decimal GetResult(IProduct product, IPriceModifierBuilder priceModifiers);
	}
}
