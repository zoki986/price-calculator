using PriceCalculator.PriceModifiersModels;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculator.Interfaces
{
	public interface IProductModifiersBuilder
	{
		List<IPriceModifier> ProductPriceModifiers { get; }
		IFormatProvider CurrencyFormat { get; }
		IDiscountCalculationMode DiscountCalculationMode { get; }
		DiscountCap DiscountCap { get; }
		int CalculationPrecision { get; set; }
		int ReportPrecision { get; set; }
	}
}
