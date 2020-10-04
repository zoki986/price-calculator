using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface IDiscount : IPriceModifier
	{
		decimal Amount { get;}
		int Precision { get; }
	}
}