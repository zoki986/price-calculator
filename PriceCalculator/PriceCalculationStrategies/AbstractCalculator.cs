using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.PriceCalculationStrategies
{
	public abstract class AbstractCalculator
	{
		protected abstract decimal ApplyTax();
		protected abstract decimal SumAdditionalExpenses();
		protected abstract IProduct ApplyPrecedenceDiscount();
		protected abstract decimal ApplyRegularDiscounts();
		protected abstract decimal CalculateTotal(Cost costs);
		protected virtual void BuildPriceCalculationResult(Cost costs, decimal total) { }
	}
}
