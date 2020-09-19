using PriceCalculator.Models;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculator.Interfaces
{
	public interface ICalculationResult
	{
		PriceCalculationResult WithTax(decimal tax);
		PriceCalculationResult WithInitialPrice(decimal price);
		PriceCalculationResult WithDiscounts(decimal discount);
		PriceCalculationResult WithExpenses(IEnumerable<IExpense> expenses);
		PriceCalculationResult WithTotal(decimal total);
		PriceCalculationResult WithFormat(NumberFormatInfo currencyFormat);
	}
}
