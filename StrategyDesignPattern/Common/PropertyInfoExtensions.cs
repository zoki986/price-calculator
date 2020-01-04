using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StrategyDesignPattern.Common
{
	public static class PropertyInfoExtensions
	{
		public static T GetValueAsType<T>(this PropertyInfo property, PriceCalculationResult calculationResult) where T : class
		{
			return property.GetValue(calculationResult) as T;
		}

		public static IEnumerable<T> GetPropertyOfType<T>(this PropertyInfo property, PriceCalculationResult result) where T : class
		{
			var values = (property.GetValue(result) as IEnumerable<T>);
			return new List<T>(values);
		}

		public static bool IsOfType<T>(this PropertyInfo property, PriceCalculationResult result)
		{
			return property.PropertyType.Equals(typeof(T));
		}
	}
}
