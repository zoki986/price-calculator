using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System;
using System.Globalization;

namespace PriceCalculator.PriceModifiersModels
{
	public class Expense : IExpense
	{
		public string Name { get; }
		public Money ExpenseAmount { get; private set; }
		public IExpenseType ExpenseType { get; set; }
		public Expense(string name, IExpenseType costType)
		{
			Name = name;
			ExpenseType = costType;
		}

		public decimal ApllyModifier(IProduct product)
		{
			ExpenseAmount = new Money(ExpenseType.GetCostAmount(product.Price.Amount));
			return ExpenseAmount.Amount;
		}

		public string AsString(IFormatProvider formatProvider)
		{
			NumberFormatInfo numberFormatInfo = new NumberFormatInfo();

			if (formatProvider is NumberFormatInfo format)
			{
				numberFormatInfo.CurrencyPositivePattern = format.CurrencyPositivePattern;
				numberFormatInfo.CurrencyDecimalDigits = ExpenseType.DecimalsInFormat;
				numberFormatInfo.CurrencySymbol = format.CurrencySymbol;
			}

			return $"{Name} = {ExpenseAmount.AsString(numberFormatInfo)}";
		}
	}
}
