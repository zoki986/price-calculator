using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.PriceModifiersModels
{
	public class Expense : IExpense
	{
		public string Name { get;}
		public IExpenseType ExpenseType { get; set; }
		public Expense(string name, IExpenseType costType)
		{
			Name = name;
			ExpenseType = costType;
		}

		public decimal ApllyModifier(IProduct product)
		  => ExpenseType.GetCostAmount(product.Price.Amount);

		public string AsString(PriceCalculationResult res) 
			=> $"{res.GetExpenseAmountFormated(this)}";

		public override string ToString() => Name;
	}
}
