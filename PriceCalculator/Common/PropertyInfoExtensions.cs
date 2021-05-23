using PriceCalculator.Models;
using System.Collections.Generic;
using System.Reflection;

namespace PriceCalculator.Common
{
	public static class PropertyInfoExtensions
	{
		public static object GetValueAsType(this PropertyInfo property, PriceCalculationResult calculationResult) 
			=> property.GetValue(calculationResult);

		public static IEnumerable<T> GetPropertyOfType<T>(this PropertyInfo property, PriceCalculationResult result) where T : class
		{
			var values = (property.GetValue(result) as IEnumerable<T>);
			return new List<T>(values);
		}

		public static bool IsOfType<T>(this PropertyInfo property)
			=> property.PropertyType.Equals(typeof(T));
	}
}
