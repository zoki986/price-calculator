using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using System;
using System.Collections.Generic;

namespace StrategyDesignPattern.Common
{
	public static class CurrencyExtensions
	{
		public static IMoney Substract(this IMoney value, IMoney other)
			=> (IMoney)Activator.CreateInstance(value.GetType(), (value.Ammount) - (other.Ammount));

		public static IMoney SubstractMany(this IMoney price, IEnumerable<IMoney> other)
		{
			foreach (var money in other)
			{
				price = price.Substract(money);
			}

			return (IMoney)Activator.CreateInstance(price.GetType(), price.Ammount);
		}

		public static IMoney Sum(this IEnumerable<IMoney> other)
		{
			decimal total = 0;
			foreach (var money in other)
			{
				total += money.Ammount;
			}

			return (IMoney)Activator.CreateInstance(typeof(Dolar), total);
		}

		public static IMoney SumWith(this IMoney value, IMoney other)
		{
			if (other == null)
				return value;

			return (IMoney)Activator.CreateInstance(value.GetType(), (value.Ammount) + (other.Ammount));
		}
		public static IMoney WithDiscountCap(this IMoney money, DiscountCap cap, IProduct product)
			 => cap == null ? money : cap.GetMaxDiscount(money, product);

		public static IMoney WithPrecision(this IMoney money, int precision)
		{
			var amount = decimal.Round(money.Ammount, precision, MidpointRounding.AwayFromZero);
			return (IMoney)Activator.CreateInstance(money.GetType(), amount);
		}

	}
}
