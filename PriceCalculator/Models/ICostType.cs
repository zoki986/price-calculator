using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public interface ICostType
	{
		decimal GetCostAmount(decimal cost, decimal price);
		string GetCostFormated(PriceCalculationResult result, IExpense expense);
	}
}
