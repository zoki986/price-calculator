using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface IProductTax : IPriceModifier
	{
		decimal Cost { get; }
		int Precision { get; }
	}
}
