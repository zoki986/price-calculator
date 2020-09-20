using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class MonetaryCost : ICostType
	{
		public MonetaryCost()
		{
		}

		public decimal GetCostAmount(decimal cost, decimal price)
			=> cost;

		public string GetCostFormated(PriceCalculationResult result, IExpense expense)
			=> $"{expense} = {expense.Cost.FormatDecimal(result.currencyFormat)}";

		public override string ToString() => "Monetary";
	}
}
