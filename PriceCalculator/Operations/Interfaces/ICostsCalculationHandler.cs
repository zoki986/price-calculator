using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.Operations.Interfaces
{
	public interface ICostsCalculationHandler 
	{
		ITaxHandler TaxHandler { get; }
		IDiscountHandler DiscountHandler { get; }
		IAdditionalExpenseHandler ExpenseHandler { get; }
		ITotalCostHandler TotalCostHandler { get; }
		Costs GetCosts(IProduct product, IPriceModifierBuilder priceModifiers, IProduct precedenceDiscountProduct);
	}
}
