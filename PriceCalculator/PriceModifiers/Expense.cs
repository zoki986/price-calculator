using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.PriceModifiers
{
	public class Expense : IExpense
	{
		public string Name { get; set; }
		public ICostType ExpenseType { get; set; }
		public decimal Cost { get; set; }
		public Expense(string name, decimal amount, ICostType costType)
		{
			Name = name;
			ExpenseType = costType;
			Cost = amount;
		}

		public decimal ApllyPriceOperation(IProduct product)
		  => ExpenseType.GetCostAmount(Cost, product.Price);

		public string AsString(PriceCalculationResult res) 
			=> $"{res.GetExpenseAmountFormated(this)}";

		public override string ToString() => Name;
	}
}
