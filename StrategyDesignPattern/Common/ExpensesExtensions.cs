using StrategyDesignPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyDesignPattern.Common
{
	public static class ExpensesExtensions
	{
		public static IEnumerable<IMoney> SumExpenses(this IEnumerable<IExpense> expenses, IProduct product)
		{
			if (expenses == null)
				yield break;

			foreach (var ex in expenses)
				yield return ex.ApllyPriceModifier(product);
		}

	}
}
