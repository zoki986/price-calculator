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
		{
			product.Price =
				 priceModifiers
				.ProductPriceModifiers
				.OfType<IPrecedenceDiscount>()
				.ApplyPrecedence(product, priceModifiers);

			return product;
		}

		public static ProductCosts ApplyTax(this IProduct product, ModifiersBuilder priceModifiers)
		{
			decimal taxAmount = priceModifiers.ProductPriceModifiers.OfType<IProductTax>().ApplyPriceOperation(product);
			return new ProductCosts().WithTax(taxAmount);
		}
		public static ProductCosts ApplyDiscounts(this ProductCosts costs, IProduct product, ModifiersBuilder priceModifiers)
		{
			decimal discountsSum =
					priceModifiers
					.ProductPriceModifiers
					.OfType<IDiscount>()
					.Where(discount => !(discount is IPrecedenceDiscount))
					.WithDiscountCalculationStrategy(priceModifiers.DiscountCalculationMode, product)
					.WithDiscountCap(priceModifiers.DiscountCap, product);

			return new ProductCosts(costs).WithDiscounts(discountsSum);
		}

		public static ProductCosts ApplyExpenses(this ProductCosts costs, IProduct product, ModifiersBuilder priceModifiers)
		{
			decimal aditionalExpenses =
					priceModifiers
					.ProductPriceModifiers
					.OfType<IExpense>()
					.SumExpenses(product);

			return new ProductCosts(costs).WithExpenses(aditionalExpenses);
		}

		public static ProductCosts CalculateTotal(this ProductCosts costs, IProduct product, ModifiersBuilder priceModifiers)
		{
			decimal finalPrice = product
					   .Price
					   .Add(costs.Tax)
					   .Add(costs.Expenses)
					   .Substract(costs.Discounts)
					   .WithPrecision(priceModifiers.CalculationPrecision);

			return new ProductCosts(costs).WithTotal(finalPrice);
		}
	}
}
