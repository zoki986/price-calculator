using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiers;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class ExpensesExtensions
	{
		public static decimal SumExpenses(this IEnumerable<IExpense> expenses, IProduct product)
		{
			return expenses.Sum(expense => expense.ApllyPriceModifier(product));
		}

		public static string GetExpenseAmountFormated(this PriceCalculationResult result, Expense expense)
		{
			return expense.ExpenseType == CostType.Monetary
				? $"{expense} = {expense.Cost.FormatDecimal(result.currencyFormat)}"
				: $"{expense} - {(expense.Cost * 100).WithPrecision(0)} % = {expense.ApllyPriceModifier(result.product).FormatDecimal(result.currencyFormat)}";
		}
	}
}
