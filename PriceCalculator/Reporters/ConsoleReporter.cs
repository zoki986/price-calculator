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
					WriteAdditionalExpensesToConsole(result);
				}
				else
				{
					WritePriceToConsole(prop, result);
				}
			}
			WriteLine();
		}

		private static void WriteAdditionalExpensesToConsole(PriceCalculationResult result)
		{
			foreach (var expense in result.Expenses)
			{
				WriteLine($"{expense.AsString(result)}");
			}
		}

		private static void WritePriceToConsole(PropertyInfo property, PriceCalculationResult result)
		{
			var propValue = property.GetValueAsType(result);
			if (property.Name.Equals(nameof(result.Discounts)) && propValue == 0)
				return;

			WriteLine($"{property.Name} - {propValue.FormatCurrency(result.currencyFormat)}");
		}
	}
}
