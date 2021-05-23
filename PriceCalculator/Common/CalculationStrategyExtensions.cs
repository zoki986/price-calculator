using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiersModels;
using System.Linq;

namespace PriceCalculator.Common
{
	public static class CalculationStrategyExtensions
	{
		public static IProduct CalculatePrecedenceDiscount(this IProduct product, IProductModifiersBuilder priceModifiers)
		{
			Money price = priceModifiers
						.GetModifiersOfType<IPrecedenceDiscount>()
						.ApplyPrecedence(product, priceModifiers)
						.WithPrecision(priceModifiers.CalculationPrecision);
			
			return product.WithNewPrice(price);
		}

		public static ProductCostsReport CalculateTax(this IProduct product, IProductModifiersBuilder priceModifiers)
		{
			decimal taxAmount = priceModifiers.ProductPriceModifiers.OfType<IProductTax>().ApplyPriceOperation(product);
			return new ProductCostsReport(new Money(taxAmount));
		}
		public static ProductCostsReport CalculateDiscounts(this ProductCostsReport costs, IProduct product, IProductModifiersBuilder priceModifiers)
		{
			decimal discountsSum =
					priceModifiers
					.GetDiscountsWithoutPrecedenceDiscount()
					.ApplyDiscountCalculationStrategy(priceModifiers.DiscountCalculationMode, product)
					.ApplyDiscountCap(priceModifiers.DiscountCap, product)
					.WithPrecision(priceModifiers.CalculationPrecision);

			return costs.WithDiscounts(new Money(discountsSum));
		}

		public static ProductCostsReport CalculateExpenses(this ProductCostsReport costs, IProduct product, IProductModifiersBuilder priceModifiers)
		{
			var aditionalExpenses =
					priceModifiers
					.GetModifiersOfType<IExpense>()
					.SumExpenses(product)
					.WithPrecision(priceModifiers.CalculationPrecision);

			return costs.WithExpenses(aditionalExpenses);
		}

		public static ProductCostsReport CalculateTotal(this ProductCostsReport costs, IProduct product, IProductModifiersBuilder priceModifiers)
		{
			var finalPrice = (product.Price + costs.Tax + costs.Expenses - costs.Discounts)
						   .WithPrecision(priceModifiers.CalculationPrecision)
						   .WithFormat(priceModifiers.CurrencyFormat);

			return costs.WithTotal(finalPrice);
		}
	}
}
