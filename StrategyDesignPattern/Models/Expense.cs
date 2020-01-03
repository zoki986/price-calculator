using StrategyDesignPattern.Common;
using StrategyDesignPattern.Interfaces;
using System;

namespace StrategyDesignPattern.Models
{
	public class Expense : IExpense
	{
		public string Name { get; set; }
		public ValueType ExpenseType { get; }
		public decimal Cost { get; set; }
		public int Precision { get; set; } = 2;
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

			var amount = (Cost * product.Price.Ammount);
			return (IMoney)Activator.CreateInstance(moneyType, amount);
		}

		public override string ToString()
		{
			return $"{Name} = {Cost}";
		}
	}
}
