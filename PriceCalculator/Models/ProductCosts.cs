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

		public ProductCosts(ProductCosts costs)
		{
			Tax = costs.Tax;
			Discounts = costs.Discounts;
			Expenses = costs.Expenses;
			Total = costs.Total;
		}
		public ProductCosts()
		{
		}
		public ProductCosts WithTax(decimal tax)
		{
			Tax = tax;
			return this;
		}

		public ProductCosts WithExpenses(decimal expenses)
		{
			Expenses = expenses;
			return this;
		}

		public ProductCosts WithDiscounts(decimal discounts)
		{
			Discounts = discounts;
			return this;
		}

		public ProductCosts WithTotal(decimal total)
		{
			Total = total;
			return this;
		}

		public decimal Tax { get; set; }
		public decimal Discounts { get; set; }
		public decimal Expenses { get; set; }
		public decimal Total { get; set; }
	}
}
