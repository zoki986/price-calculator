using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class Cost
	{
		public Cost(decimal tax, decimal discounts, decimal expenses)
		{
			Tax = tax;
			Discounts = discounts;
			Expenses = expenses;
		}

		public decimal Tax { get; }
		public decimal Discounts { get; }
		public decimal Expenses { get; }
	}
}
