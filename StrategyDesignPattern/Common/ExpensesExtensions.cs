using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using System.Collections.Generic;
using System.Linq;

namespace StrategyDesignPattern.Common
{
	public static class ExpensesExtensions
	{
		public static Money SumExpenses(this IEnumerable<IExpense> expenses, IProduct product)
		{
			var total = expenses.Sum(expense => expense.ApllyPriceModifier(product).Amount);
			return new Money(total);
		}
	}
}
