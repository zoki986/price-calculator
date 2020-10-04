using PriceCalculator.Common;
using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class MonetaryCost : IExpenseType
	{
		private readonly decimal amount;

		public MonetaryCost(decimal amount)
		{
			this.amount = amount;
		}

		public decimal GetCostAmount(decimal price)
			=> amount;

		public string GetCostFormated(PriceCalculationResult result, IExpense expense)
			=> $"{expense} = {amount.FormatDecimal(result.currencyFormat)}";

		public override string ToString() => "Monetary";
	}
}
