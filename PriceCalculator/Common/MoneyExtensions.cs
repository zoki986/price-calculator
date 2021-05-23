using PriceCalculator.Models;
using System;

namespace PriceCalculator.Common
{
	public static class MoneyExtensions
	{
		public static Money WithPrecision(this Money money, int precision)
			=> new Money(decimal.Round(money.Amount, precision, MidpointRounding.AwayFromZero));
		public static Money WithFormat(this Money money, IFormatProvider format)
		{
			return new Money(money.Amount, format);
		}
	}
}
