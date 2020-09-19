using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiers;

namespace Tests
{
	public static class PriceDependencies
	{
		public static IProductTax GetTaxWithAmount(decimal amount)
			=> new TaxPriceModifier(amount);

		public static IProduct GetSimpleProduct()
			=> new Book("The Little Prince", 12345, 20.25M);

		public static IExpense GetExpense(string name, decimal value, CostType type)
			=> new Expense(name, value, type);
	}
}
