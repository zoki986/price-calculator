using PriceCalculator.CalculationModes;
using PriceCalculator.PriceModifiersModels;
using System;
using System.Collections.Generic;

namespace PriceCalculator.Interfaces
{
	public interface IProductModifiersBuilder
	{
		List<IPriceModifier> ProductPriceModifiers { get; }
		IFormatProvider CurrencyFormat { get; }
		IDiscountCalculationMode DiscountCalculationMode { get; }
		DiscountCap DiscountCap { get; }
		int CalculationPrecision { get; set; }
	}
}
