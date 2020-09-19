namespace PriceCalculator.Interfaces
{
	public interface IPriceModifier
	{
		decimal ApllyPriceOperation(IProduct product);
	}
}