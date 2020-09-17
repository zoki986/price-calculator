using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.Operations.Interfaces
{
	public interface IAdditionalExpensesHandler
	{
		ITaxHandler TaxHandler { get; }
		IDiscountHandler DiscountHandler { get; }
		IAdditionalExpenseHandler ExpenseHandler { get; }
		ITotalCostHandler TotalCostHandler { get; }
		ProductCosts GetAdditionalCosts(IProduct product, IPriceModifierBuilder priceModifiers, IProduct precedenceDiscountProduct);
	}
}
