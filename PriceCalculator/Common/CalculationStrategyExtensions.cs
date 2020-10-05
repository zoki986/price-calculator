using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiersModels;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class CalculationStrategyExtensions
	{
		public static IProduct ApplyPrecedenceDiscount(this IProduct product, ModifiersBuilder priceModifiers)
			=>	product.WithPrice(
				 priceModifiers
				.ProductPriceModifiers
				.OfType<IPrecedenceDiscount>()
				.ApplyPrecedence(product, priceModifiers));

		public static ProductCostsReport ApplyTax(this IProduct product, ModifiersBuilder priceModifiers)
		{
			decimal taxAmount = priceModifiers.ProductPriceModifiers.OfType<IProductTax>().ApplyPriceOperation(product);
			return new ProductCostsReport().WithTax(new Money(taxAmount));
		}
		public static ProductCostsReport ApplyDiscounts(this ProductCostsReport costs, IProduct product, ModifiersBuilder priceModifiers)
		{
			decimal discountsSum =
					priceModifiers
					.ProductPriceModifiers
					.OfType<IDiscount>()
					.Where(discount => !(discount is IPrecedenceDiscount))
					.ApplyDiscountCalculationStrategy(priceModifiers.DiscountCalculationMode, product)
					.ApplyDiscountCap(priceModifiers.DiscountCap, product);

			return new ProductCostsReport(costs).WithDiscounts(new Money(discountsSum));
		}

		public static ProductCostsReport ApplyExpenses(this ProductCostsReport costs, IProduct product, ModifiersBuilder priceModifiers)
		{
			var aditionalExpenses =
					priceModifiers
					.ProductPriceModifiers
					.OfType<IExpense>()
					.SumExpenses(product);

			return new ProductCostsReport(costs).WithExpenses(aditionalExpenses);
		}

		public static ProductCostsReport CalculateTotal(this ProductCostsReport costs, IProduct product, ModifiersBuilder priceModifiers)
		{
			var finalPrice = 
						product.Price + costs.Tax  + costs.Expenses - costs.Discounts
					   .WithPrecision(priceModifiers.CalculationPrecision);

			return new ProductCostsReport(costs).WithTotal(finalPrice);
		}
	}
}
