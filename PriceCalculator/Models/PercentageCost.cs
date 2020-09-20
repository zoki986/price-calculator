using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class PercentageCost : ICostType
	{
		public PercentageCost()
		{
		}

		public decimal GetCostAmount(decimal cost, decimal price)
			=> cost * price;

		public string GetCostFormated(PriceCalculationResult result, IExpense expense)
			=> $"{expense} - {(expense.Cost * 100).WithPrecision(0)} " +
			$"% = {expense.ApllyPriceOperation(result.product).FormatDecimal(result.currencyFormat)}";

		public override string ToString() => "Percentage";
	}
}
