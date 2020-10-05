using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiersModels;
using System;
using System.Globalization;

namespace PriceCalculator.Common
{
	public static class DecimalExtensions
	{
		public static decimal WithPrecision(this decimal number, int precision)
			=> decimal.Round(number, precision, MidpointRounding.AwayFromZero);
		public static Money WithPrecision(this Money number, int precision)
			=> new Money(decimal.Round(number.Amount, precision, MidpointRounding.AwayFromZero));
		public static string FormatDecimal(this decimal number, NumberFormatInfo formatInfo)
			=> number.ToString("C", formatInfo);
		public static string FormatDecimal(this Money number, NumberFormatInfo formatInfo)
			=> number.Amount.ToString("C", formatInfo);
		public static decimal Substract(this decimal value, decimal other)
			=> decimal.Subtract(value, other);
		public static decimal ApplyDiscountCap(this decimal amount, DiscountCap cap, IProduct product)
			=> cap == null ? amount : cap.GetMaxDiscount(amount, product);
		public static decimal Add(this decimal value, decimal other)
			=> value + other;
	}
}
