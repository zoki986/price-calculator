using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiersModels;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class CalculationStrategyExtensions
	{
		public static IProduct CalculatePrecedenceDiscount(this IProduct product, IProductModifiersBuilder priceModifiers)
			=> product.WithPrice(
				 priceModifiers
				.ProductPriceModifiers
				.OfType<IPrecedenceDiscount>()
				.ApplyPrecedence(product, priceModifiers)
				.WithPrecision(priceModifiers.CalculationPrecision)
				);

		public static ProductCostsReport CalculateTax(this IProduct product, IProductModifiersBuilder priceModifiers)
		{
			decimal taxAmount = priceModifiers.ProductPriceModifiers.OfType<IProductTax>().ApplyPriceOperation(product);
			return new ProductCostsReport().WithTax(new Money(taxAmount));
		}
		public static ProductCostsReport CalculateDiscounts(this ProductCostsReport costs, IProduct product, IProductModifiersBuilder priceModifiers)
		{
			decimal discountsSum =
					priceModifiers
					.ProductPriceModifiers
					.OfType<IDiscount>()
					.Where(discount => !(discount is IPrecedenceDiscount))
					.ApplyDiscountCalculationStrategy(priceModifiers.DiscountCalculationMode, product)
					.ApplyDiscountCap(priceModifiers.DiscountCap, product)
					.WithPrecision(priceModifiers.CalculationPrecision);

			return new ProductCostsReport(costs).WithDiscounts(new Money(discountsSum));
		}

		public static ProductCostsReport CalculateExpenses(this ProductCostsReport costs, IProduct product, IProductModifiersBuilder priceModifiers)
		{
			var aditionalExpenses =
					priceModifiers
					.ProductPriceModifiers
					.OfType<IExpense>()
					.SumExpenses(product)
					.WithPrecision(priceModifiers.CalculationPrecision);

			return new ProductCostsReport(costs).WithExpenses(aditionalExpenses);
		}

		public static ProductCostsReport CalculateTotal(this ProductCostsReport costs, IProduct product, IProductModifiersBuilder priceModifiers)
		{
			var finalPrice = 
						product.Price + costs.Tax  + costs.Expenses - costs.Discounts
					   .WithPrecision(priceModifiers.CalculationPrecision);

			return new ProductCostsReport(costs).WithTotal(finalPrice);
		}
	}
}
