using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class ExpensesExtensions
	{
		public static Money SumExpenses(this IEnumerable<IExpense> expenses, IProduct product)
			=> new Money(expenses.Sum(expense => expense.ApllyModifier(product)));
	}
}
