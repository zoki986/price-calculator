using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Operations.Interfaces;

namespace PriceCalculator.Operations.Implementations
{
	public class AdditionalExpensesHandler : IAdditionalExpenseHandler
	{
		public decimal GetResult(IProduct product, IPriceModifierBuilder priceModifiers)
		{
			return priceModifiers.AdditionalExpenses
				   .SumExpenses(product);
		}
	}
}
