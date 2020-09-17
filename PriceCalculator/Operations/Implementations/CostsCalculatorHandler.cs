using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.Operations.Interfaces;

namespace PriceCalculator.Operations.Implementations
{
	public class CostsCalculatorHandler : ICostsCalculationHandler
	{
		public ITaxHandler TaxHandler { get; private set; } = new TaxHandler();
		public IDiscountHandler DiscountHandler { get; private set; } = new DiscountHandler();
		public IAdditionalExpenseHandler ExpenseHandler { get; private set; } = new AdditionalExpensesHandler();
		public ITotalCostHandler TotalCostHandler { get; private set; } = new TotalCostHandler();

		public CostsCalculatorHandler WithTaxHandler(ITaxHandler handler)
		{
			TaxHandler = handler;
			return this;
		}

		public CostsCalculatorHandler WithDiscountHandler(IDiscountHandler handler)
		{
			DiscountHandler = handler;
			return this;
		}
		public CostsCalculatorHandler WithExpenseHandler(IAdditionalExpenseHandler handler)
		{
			ExpenseHandler = handler;
			return this;
		}
		public CostsCalculatorHandler WithTotalCostHandler(ITotalCostHandler handler)
		{
			TotalCostHandler = handler;
			return this;
		}

		public ProductCosts GetProductCosts(IProduct product, IPriceModifierBuilder priceModifiers, IProduct precedenceDiscountProduct)
		{
			var tax = TaxHandler.GetResult(precedenceDiscountProduct, priceModifiers);
			var discounts = DiscountHandler.GetResult(product, priceModifiers, precedenceDiscountProduct);
			var expenses = ExpenseHandler.GetResult(product, priceModifiers);

			return TotalCostHandler.GetResult(precedenceDiscountProduct, new ProductCosts(tax, discounts, expenses));
		}
	}
}
