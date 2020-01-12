using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class Costs
	{
		public Costs(decimal tax, decimal discounts, decimal expenses)
		{
			Tax = tax;
			Discounts = discounts;
			Expenses = expenses;
		}

		public Costs(Costs costs, decimal total)
		{
			Tax = costs.Tax;
			Discounts = costs.Discounts;
			Expenses = costs.Expenses;
			Total = total;
		}

		public decimal Tax { get; }
		public decimal Discounts { get; }
		public decimal Expenses { get; }
		public decimal Total { get; }
	}
}
