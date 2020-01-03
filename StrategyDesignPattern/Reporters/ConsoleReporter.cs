using StrategyDesignPattern.Common;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace StrategyDesignPattern.Reporters
{
	public static class ConsoleReporter
	{
		public static void PrintResult(PriceCalculationResult priceCalculationResult)
		{
			WriteLine($"Reporting product price for product: {priceCalculationResult.product}");

			var props = priceCalculationResult.GetType().GetProperties();
			foreach (var prop in props)
			{
				if (prop.PropertyType == typeof(IEnumerable<IExpense>))
				{
					var expenses = (prop.GetValue(priceCalculationResult) as IEnumerable<Expense>) ?? Enumerable.Empty<Expense>();

					foreach (var expense in expenses)
					{
						WriteLine(expense);
					}
				}
				else
				{
					var propValue = prop.GetValueAsType<IMoney>(priceCalculationResult);
					if (prop.Name.Equals("Discounts") && propValue.Ammount == 0)
						continue;

					Write(prop.Name + " - "); Write(propValue.ToString());
					WriteLine();
				}
			}
			WriteLine();
		}
	}
}
