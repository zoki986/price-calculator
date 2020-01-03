using StrategyDesignPattern.Models;
using System.Reflection;

namespace StrategyDesignPattern.Common
{
	public static class PropertyInfoExtensions
	{
		public static T GetValueAsType<T>(this PropertyInfo property, PriceCalculationResult calculationResult) where T : class
		{
			return property.GetValue(calculationResult) as T;
		}
	}
}
