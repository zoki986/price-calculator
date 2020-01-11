using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
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

		public decimal ApllyPriceModifier(IProduct product)
		{
			if (ExpenseType == ValueType.Monetary)
				return Cost;

			return (Cost * product.Price);
		}

		public string AsString(PriceCalculationResult res)
		{
			return  $"{res.GetExpenseAmountFormated(this)}";
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
