namespace PriceCalculator.Interfaces
{
	public interface IProductTax : IPriceModifier
	{
		decimal Amount { get; }
	}
}
