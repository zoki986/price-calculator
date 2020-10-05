using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiersModels;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class DiscountExtensions
	{
		public static Money ApplyPrecedence(this IEnumerable<IPrecedenceDiscount> discounts, IProduct product, IPriceModifierBuilder priceModifiers)
		{
			var precedenceDiscountSum = discounts
										.SumPrecedenceDiscounts(product)
										.ApplyDiscountCap(priceModifiers.DiscountCap, product);
			
			return product.Price - precedenceDiscountSum;
		}

		public static decimal ApplyPriceOperation(this IEnumerable<IProductTax> taxes, IProduct product)
			=> taxes.Select(x => x.ApllyModifier(product)).Sum();

		public static decimal SumDiscounts(this IEnumerable<IDiscount> discounts, IProduct product) 
			=> discounts.Cast<Discount>().Aggregate(0M, (prev, next) => prev + next.ApllyModifier(product));

		public static decimal SumPrecedenceDiscounts(this IEnumerable<IPrecedenceDiscount> discounts, IProduct product) 
			=> discounts.OfType<Discount>().Aggregate(0M, (prev, next) => prev + next.ApllyModifier(product));

		public static decimal ApplyDiscountCalculationStrategy(this IEnumerable<IDiscount> discounts, IDiscountCalculationMode strategy, IProduct product)
			=> strategy.CalculateDiscounts(discounts, product).Amount;
	}
}
