using StrategyDesignPattern.Interfaces;
using System;

namespace StrategyDesignPattern.Common
{
	public static class ProductExtensions
	{
		public static IProduct GetProductWithNewPrice(this IProduct product, IMoney price)
			=> (IProduct)Activator.CreateInstance(product.GetType(), product.Name, product.UPC, price);
	}
}
