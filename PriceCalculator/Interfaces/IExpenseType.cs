using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface IExpenseType
	{
		decimal GetCostAmount(decimal price);
		string GetCostFormated(PriceCalculationResult result, IExpense expense);
	}
}
