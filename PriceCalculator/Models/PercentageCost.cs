using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class PercentageCost : IExpenseType
	{
		private readonly decimal amount;
		public PercentageCost(decimal amount)
		{
			this.amount = amount;
		}

		public decimal GetCostAmount(decimal price)
			=> amount * price;

		public string GetCostFormated(PriceCalculationResult result, IExpense expense)
			=> $"{expense} - {(amount * 100).WithPrecision(0)} " +
			$"% = {expense.ApllyPriceOperation(result.product).FormatDecimal(result.currencyFormat)}";

		public override string ToString() => "Percentage";
	}
}
