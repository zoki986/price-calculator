using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiers;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class CalculationStrategyExtensions
	{
		public static IProduct ApplyPrecedenceDiscount(this IProduct product, PriceModifiersBuilder priceModifiers)
		{
			product.Price =
				 priceModifiers
				.ProductOperations
				.OfType<IPrecedenceDiscount>()
				.ApplyPrecedence(product, priceModifiers);

			return product;
		}

		public static ProductCosts ApplyTax(this IProduct product, PriceModifiersBuilder priceModifiers)
		{
			decimal taxAmount = priceModifiers.ProductOperations.OfType<IProductTax>().ApplyPriceOperation(product);
			return new ProductCosts().WithTax(taxAmount);
		}
		public static ProductCosts ApplyDiscounts(this ProductCosts costs, IProduct product, PriceModifiersBuilder priceModifiers)
		{
			decimal discountsSum =
					priceModifiers
					.ProductOperations
					.OfType<IDiscount>()
					.Where(po => po.GetType().GetInterface(nameof(IPrecedenceDiscount)) == null)
					.WithDiscountCalculationStrategy(priceModifiers.DiscountCalculationMode, product)
					.WithDiscountCap(priceModifiers.DiscountCap, product);

			return new ProductCosts(costs).WithDiscounts(discountsSum);
		}

		public static ProductCosts ApplyExpenses(this ProductCosts costs, IProduct product, PriceModifiersBuilder priceModifiers)
		{
			decimal aditionalExpenses =
					priceModifiers
					.ProductOperations
					.OfType<IExpense>()
					.SumExpenses(product);

			return new ProductCosts(costs).WithExpenses(aditionalExpenses);
		}

		public static ProductCosts CalculateTotal(this ProductCosts costs, IProduct product, PriceModifiersBuilder priceModifiers)
		{
			decimal finalPrice = product
					   .Price
					   .Add(costs.Tax)
					   .Add(costs.Expenses)
					   .Substract(costs.Discounts)
					   .WithPrecision(Constants.DefaultPrecision);

			return new ProductCosts(costs).WithTotal(finalPrice);
		}
	}
}
