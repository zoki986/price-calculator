namespace PriceCalculator.Models
{
	public class ProductCostsReport
	{
		public ProductCostsReport(ProductCostsReport costs)
		{
			Tax = costs.Tax;
			Discounts = costs.Discounts;
			Expenses = costs.Expenses;
			Total = costs.Total;
		}
		public ProductCostsReport()
		{
		}
		public ProductCostsReport WithTax(Money tax)
		{
			Tax = tax;
			return this;
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

		public Money Tax { get; set; }
		public Money Discounts { get; set; }
		public Money Expenses { get; set; }
		public Money Total { get; set; }
	}
}
