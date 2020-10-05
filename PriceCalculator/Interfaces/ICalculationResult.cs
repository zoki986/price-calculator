using PriceCalculator.Models;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculator.Interfaces
{
	public interface ICalculationResult
	{
		PriceCalculationResult WithTax(Money tax);
		PriceCalculationResult WithInitialPrice(Money price);
		PriceCalculationResult WithDiscounts(Money discount);
		PriceCalculationResult WithExpenses(IEnumerable<IExpense> expenses);
		PriceCalculationResult WithTotal(Money total);
		PriceCalculationResult WithFormat(NumberFormatInfo currencyFormat);
	}
}
