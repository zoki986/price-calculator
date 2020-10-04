using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiersModels;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class ExpensesExtensions
	{
		public static decimal SumExpenses(this IEnumerable<IExpense> expenses, IProduct product)
			=> expenses.Sum(expense => expense.ApllyPriceOperation(product));
		public static string GetExpenseAmountFormated(this PriceCalculationResult result, Expense expense)
			=> expense.ExpenseType.GetCostFormated(result, expense);
	}
}
