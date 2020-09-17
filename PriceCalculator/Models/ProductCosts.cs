using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class ProductCosts
	{
		public ProductCosts(decimal tax, decimal discounts, decimal expenses, decimal total)
		{
			Tax = tax;
			Discounts = discounts;
			Expenses = expenses;
			Total = total;
		}

		public ProductCosts(ProductCosts costs, decimal total)
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
