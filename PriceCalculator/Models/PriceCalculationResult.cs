using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalculator.Models
{
	public class PriceCalculationResult
	{
		public IProduct Product { get; }
		public Money InitialPrice { get;  }
		public Money Tax { get; }
		public Money Discounts { get; }
		public Money ExpensesAmount { get; }
		public Money Total { get;  }
		public IFormatProvider CurrencyFormat { get; }
		public IEnumerable<IExpense> Expenses { get; }
		public PriceCalculationResult(IProduct product, IProductModifiersBuilder productModifiers, ProductCostsReport productCosts)
		{
			this.Product = product;
			this.Expenses = productModifiers.GetModifiersOfType<IExpense>();
			this.CurrencyFormat = productModifiers.CurrencyFormat;
			this.InitialPrice = product.Price;
			(this.Tax, this.Discounts, this.ExpensesAmount, this.Total) = productCosts;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.TryAppendLine("Cost", Product.Price, CurrencyFormat);
			sb.TryAppendLine("Tax", Tax, CurrencyFormat);
			sb.TryAppendLine("Discounts", Discounts, CurrencyFormat);

			if (Expenses != null)
			{
				foreach (var expense in Expenses)
					sb.AppendLine($"{expense.AsString(CurrencyFormat)}");
			}

			sb.TryAppendLine("TOTAL", Total, CurrencyFormat);

			return sb.ToString();
		}
	}

	public static class SbExtensions
	{
		public static StringBuilder TryAppendLine(this StringBuilder sb, string expenseName, Money amount, IFormatProvider format)
		{
			if (amount == null || (amount.Amount == default))
			{
				return sb;
			}

			return sb.AppendLine($"{expenseName} = {amount.AsString(format)}");
		}
	}
}
