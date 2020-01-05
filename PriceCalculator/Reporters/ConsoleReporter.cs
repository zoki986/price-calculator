using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using static System.Console;

namespace PriceCalculator.Reporters
{
	public static class ConsoleReporter
	{
		public static void PrintResult(PriceCalculationResult result)
		{
			WriteLine($"Reporting product price for product: ");
			WriteLine(result.product.AsString(result.currencyFormat));
			var props = result.GetType().GetProperties();

			foreach (var prop in props)
			{
				if (prop.IsOfType<IEnumerable<IExpense>>(result))
				{
					var expenses = prop.GetPropertyOfType<IExpense>(result);
					WriteAdditionalExpensesToConsole(expenses, result);
				}
				else
				{
					WritePriceToConsole(prop, result);
				}
			}
			WriteLine();
		}

		private static void WriteAdditionalExpensesToConsole(IEnumerable<IExpense> expenses, PriceCalculationResult result)
		{
			foreach (var expense in expenses)
			{
				Write($"{expense.Name} - {expense.Cost.FormatCurrency(result.currencyFormat)} {Environment.NewLine}");
			}
		}

		private static void WritePriceToConsole(PropertyInfo property, PriceCalculationResult result)
		{
			var propValue = property.GetValueAsType<IMoney>(result);
			if (property.Name.Equals(nameof(result.Discounts)) && propValue.Amount == 0)
				return;

			Write($"{property.Name} - {propValue.FormatCurrency(result.currencyFormat)} {Environment.NewLine}");
		}
	}
}
