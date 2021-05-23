using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiersModels;

namespace Tests
{
	public static class PriceDependencies
	{
		public static IProductTax GetTaxWithAmount(decimal amount)
			=> new TaxPriceModifier(amount);

		public static IProduct GetSimpleProduct(string name, int upc, decimal amount)
			=> new Book(name, upc, new Money(amount));

		public static IExpense GetExpense(string name, IExpenseType type)
			=> new Expense(name, type);
	}
}
