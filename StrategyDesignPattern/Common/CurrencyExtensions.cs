using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using StrategyDesignPattern.PriceModifiers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StrategyDesignPattern.Common
{
	public static class CurrencyExtensions
	{
		public static IMoney Substract(this IMoney value, IMoney other)
			=> (IMoney)Activator.CreateInstance(value.GetType(), (value.Amount) - (other.Amount));

		public static Money Substract(this IMoney value, Money other)
			=> new Money((value.Amount) - (other.Amount));

		public static IMoney SubstractMany(this IMoney price, IEnumerable<IMoney> other)
		{
			foreach (var money in other)
			{
				price = price.Substract(money);
			}
			
			return (IMoney)Activator.CreateInstance(price.GetType(), price.Amount);
		}

		public static IMoney Sum(this IEnumerable<IMoney> source)
		{
			var total = source.Sum(money => money.Amount);
			return (IMoney)Activator.CreateInstance(typeof(Money), total);
		}

		public static IMoney SumWith(this IMoney value, IMoney other)
		{
			if (other == null)
				return value;

			return (IMoney)Activator.CreateInstance(value.GetType(), (value.Amount) + (other.Amount));
		}
		public static IMoney WithDiscountCap(this IMoney money, DiscountCap cap, IProduct product)
			 => cap == null ? money : cap.GetMaxDiscount(money, product);

		public static IMoney WithPrecision(this IMoney money, int precision)
		{
			var amount = decimal.Round(money.Amount, precision, MidpointRounding.AwayFromZero);
			return (IMoney)Activator.CreateInstance(money.GetType(), amount);
		}

		public static string FormatCurrency(this IMoney currency, NumberFormatInfo formatInfo)
			=> currency == null ? string.Empty : currency.Amount.ToString("C",formatInfo);
	}
}
