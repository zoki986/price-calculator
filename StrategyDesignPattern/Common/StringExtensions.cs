using StrategyDesignPattern.Interfaces;

namespace StrategyDesignPattern.Common
{
	public static class StringExtensions
	{
		public static string ConcatWith(this string value, IMoney money, int precision)
			=> money.Ammount <= 0 ? string.Empty : string.Concat(value, money.ToString(), "\n");

		public static string TryConcat(this string value, object other)
		{
			return value;
		}
	}
}
