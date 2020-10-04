﻿using PriceCalculator.Interfaces;
using PriceCalculator.PriceModifiersModels;
using System;
using System.Globalization;

namespace PriceCalculator.Common
{
	public static class DecimalExtensions
	{
		public static decimal WithPrecision(this decimal number, int precision)
			=> decimal.Round(number, precision, MidpointRounding.AwayFromZero);
		public static string FormatDecimal(this decimal number, NumberFormatInfo formatInfo)
			=> number.ToString("C", formatInfo);
		public static decimal Substract(this decimal value, decimal other)
			=> decimal.Subtract(value, other);
		public static decimal WithDiscountCap(this decimal amount, DiscountCap cap, IProduct product)
			=> cap == null ? amount : cap.GetMaxDiscount(amount, product);
		public static decimal Add(this decimal value, decimal other)
			=> value + other;
	}
}
