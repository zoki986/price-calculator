using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface IExpense
	{
		string Name { get; }
		decimal Cost { get; }
		decimal ApllyPriceModifier(IProduct product);
		string AsString(PriceCalculationResult res);
	}
}
