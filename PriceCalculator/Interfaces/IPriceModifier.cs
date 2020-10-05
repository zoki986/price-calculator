namespace PriceCalculator.Interfaces
{
	public interface IPriceModifier
	{
		decimal ApllyModifier(IProduct product);
	}
}