using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface IDiscount
	{
		decimal DiscountAmount { get;}
		int Precision { get; }
		bool HasPrecedence { get; }
		decimal ApllyPriceModifier(IProduct product);
	}
}