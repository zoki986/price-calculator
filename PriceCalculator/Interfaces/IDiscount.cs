using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface IDiscount : IPriceModifier
	{
		decimal DiscountAmount { get;}
		int Precision { get; }
	}
}