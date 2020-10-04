using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface IProductTax : IPriceModifier
	{
		decimal Amount { get; }
		int Precision { get; }
	}
}
