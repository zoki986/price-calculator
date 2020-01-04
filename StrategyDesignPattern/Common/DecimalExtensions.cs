using StrategyDesignPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StrategyDesignPattern.Common
{
	public static class DecimalExtensions
	{
		public static decimal WithPrecision(this decimal number, int precision)
			=> decimal.Round(number, precision, MidpointRounding.AwayFromZero);

		public static string FormatCurrency(this decimal currency, NumberFormatInfo formatInfo)
				  => currency.ToString("C", formatInfo);
	}
}
