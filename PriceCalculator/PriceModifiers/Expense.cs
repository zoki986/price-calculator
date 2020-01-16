using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.PriceModifiers
{
	public class Expense : IExpense
	{
		public string Name { get; set; }
		public CostType ExpenseType { get; }
		public decimal Cost { get; set; }
		public Expense(string name, decimal amount, CostType costType)
		{
			Name = name;
			ExpenseType = costType;
			Cost = amount;
		}

		public decimal ApllyPriceModifier(IProduct product)
		{
			if (ExpenseType == CostType.Monetary)
				return Cost;

			return (Cost * product.Price);
		}

		public string AsString(PriceCalculationResult res)
		{
			return $"{res.GetExpenseAmountFormated(this)}";
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
