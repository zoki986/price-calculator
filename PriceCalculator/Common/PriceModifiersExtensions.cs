using PriceCalculator.Interfaces;
using PriceCalculator.PriceModifiersModels;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class PriceModifiersExtensions
	{
		public static IEnumerable<T> GetModifiersOfType<T>(this IProductModifiersBuilder productModifiers)
		{
			return productModifiers?.ProductPriceModifiers?.OfType<T>() ?? Enumerable.Empty<T>();
		}

		public static IEnumerable<IDiscount> GetDiscountsWithoutPrecedenceDiscount(this IProductModifiersBuilder productModifiers)
		{
			return productModifiers?
				.ProductPriceModifiers?
				.OfType<IDiscount>()
				.Where(discount => !(discount is IPrecedenceDiscount)) 
				?? Enumerable.Empty<IDiscount>();
		}
	}
}
