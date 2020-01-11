using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface ITax
	{
		decimal Cost { get; }
		int Precision { get; }
		decimal ApllyPriceModifier(IProduct product);
	}
}
