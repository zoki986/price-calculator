using PriceCalculator.PriceModifiersModels;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculator.Interfaces
{
	public interface IPriceModifierBuilder
	{
		List<IPriceModifier> ProductPriceModifiers { get; }
		NumberFormatInfo CurrencyFormat { get; }
		IDiscountCalculationMode DiscountCalculationMode { get; }
		DiscountCap DiscountCap { get; }
	}
}
