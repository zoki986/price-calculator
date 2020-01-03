using StrategyDesignPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyDesignPattern.Common
{
	public static class DecimalExtensions
	{
		public static decimal WithPrecision(this decimal number, int precision)
		{
			return decimal.Round(number, precision, MidpointRounding.AwayFromZero);
		}
		public static decimal SumModifiers(this decimal number, IEnumerable<IDiscount> modifiers)
		{
			return number + modifiers.Sum(m => m.Amount.Ammount);
		}
	}
}
