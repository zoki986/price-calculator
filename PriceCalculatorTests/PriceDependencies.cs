using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiers;

namespace Tests
{
	public static class PriceDependencies
	{
		public static ITax GetTaxWithAmount(decimal amount)
			=> new TaxPriceModifier(new Money(amount));

		public static IProduct GetSimpleProduct()
			=> new Book("The Little Prince", 12345, new Money(20.25M));

		public static IExpense GetExpense(string name, decimal value, ValueType type)
			=> new Expense(name, value, type);
	}
}
