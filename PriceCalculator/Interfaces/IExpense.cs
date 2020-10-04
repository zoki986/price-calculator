using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface IExpense : IPriceModifier
	{
		string Name { get; }
		string AsString(PriceCalculationResult res);
	}
}
