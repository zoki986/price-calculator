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
			=> expenses.Sum(expense => expense.ApllyPriceOperation(product));

		public static string GetExpenseAmountFormated(this PriceCalculationResult result, Expense expense) 
			=> expense.ExpenseType == CostType.Monetary
				? $"{expense} = {expense.Cost.FormatDecimal(result.currencyFormat)}"
				: $"{expense} - {(expense.Cost * 100).WithPrecision(0)} % = {expense.ApllyPriceOperation(result.product).FormatDecimal(result.currencyFormat)}";
	}
}
