namespace PriceCalculator.Interfaces
{
	public interface IDiscount : IPriceModifier
	{
		decimal Amount { get;}
	}
}