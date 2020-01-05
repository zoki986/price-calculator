using PriceCalculator.Interfaces;
using System;
using ValueType = PriceCalculator.Models.ValueType;

namespace PriceCalculator.PriceModifiers
{
	public class Expense : IExpense
	{
		public string Name { get; set; }
		public ValueType ExpenseType { get; }
		public decimal Cost { get; set; }
		public Expense(string name, decimal amount, ValueType expenseType)
		{
			Name = name;
			ExpenseType = expenseType;
			Cost = amount;
		}

		public IMoney ApllyPriceModifier(IProduct product)
		{
			var moneyType = product.Price.GetType();
			if (ExpenseType == ValueType.Monetary)
				return (IMoney)Activator.CreateInstance(moneyType, Cost);

			Cost = (Cost * product.Price.Amount);
			return (IMoney)Activator.CreateInstance(moneyType, Cost);
		}

		public override string ToString()
		{
			return $"{Name} = {Cost}";
		}
	}
}
