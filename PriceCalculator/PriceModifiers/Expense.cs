using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.PriceModifiers
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

		public decimal ApllyPriceOperation(IProduct product)
		  => ExpenseType.GetCostAmount(product.Price);

		public string AsString(PriceCalculationResult res) 
			=> $"{res.GetExpenseAmountFormated(this)}";

		public override string ToString() => Name;
	}
}
