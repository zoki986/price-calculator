using PriceCalculator.PriceModifiers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculator.Interfaces
{
	public interface IPriceModifierBuilder
	{
		ITax Tax { get; set; }
		List<IDiscount> Discounts { get; set; }
		List<IExpense> AdditionalExpenses { get; set; }
		NumberFormatInfo CurrencyFormat { get; }
		Func<IEnumerable<IDiscount>, IProduct, decimal> DiscountCalculationMode { get; }
		DiscountCap DiscountCap { get; set; }
	}
}
