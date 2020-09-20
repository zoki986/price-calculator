using PriceCalculator.PriceModifiers;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculator.Interfaces
{
	public interface IPriceModifierBuilder
	{
		List<IPriceModifier> ProductPriceModifiers { get; }
		NumberFormatInfo CurrencyFormat { get; }
		Func<IEnumerable<IDiscount>, IProduct, decimal> DiscountCalculationMode{ get; }
		DiscountCap DiscountCap { get; }
	}
}
