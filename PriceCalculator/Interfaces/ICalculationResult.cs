using PriceCalculator.Models;
using System;
using System.Collections.Generic;

namespace PriceCalculator.Interfaces
{
	public interface ICalculationResult
	{
		PriceCalculationResult WithTax(Money tax);
		PriceCalculationResult WithInitialPrice(Money price);
		PriceCalculationResult WithDiscounts(Money discount);
		PriceCalculationResult WithExpenses(IEnumerable<IExpense> expenses);
		PriceCalculationResult WithTotal(Money total);
		PriceCalculationResult WithFormat(IFormatProvider currencyFormat);
	}
}
