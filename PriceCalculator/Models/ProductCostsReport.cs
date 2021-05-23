namespace PriceCalculator.Models
{
	public class ProductCostsReport
	{
		public Money Tax { get; set; }
		public Money Discounts { get; set; }
		public Money Expenses { get; set; }
		public Money Total { get; set; }

		public void Deconstruct(out Money tax, out Money discounts, out Money expenses, out Money total)
		{
			tax = Tax;
			discounts = Discounts;
			expenses = Expenses;
			total = Total;
		}

		public ProductCostsReport(ProductCostsReport costs)
		{
			Tax = costs.Tax;
			Discounts = costs.Discounts;
			Expenses = costs.Expenses;
			Total = costs.Total;
		}

		public ProductCostsReport(Money tax)
		{
			Tax = tax;
		}
		public ProductCostsReport WithExpenses(Money expenses)
		{
			Expenses = expenses;
			return this;
		}

		public ProductCostsReport WithDiscounts(Money discounts)
		{
			Discounts = discounts;
			return this;
		}

		public ProductCostsReport WithTotal(Money total)
		{
			Total = total;
			return this;
		}
	}
}
