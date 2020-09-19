using PriceCalculator.Builder;
using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiers;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class CalculationStrategy : IPriceCalculation
	{
		public PriceCalculationResult GetPriceResultForProduct(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			ProductCosts costs = CalculateProductCosts(product, priceModifiers);

			return BuildPriceCalculationResult(product, priceModifiers, costs);
		}

		private ProductCosts CalculateProductCosts(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			product.Price = priceModifiers
				.ProductOperations
				.OfType<IPrecedenceDiscount>()
				.ApplyPrecedence(product, priceModifiers);

			decimal taxAmount = priceModifiers.ProductOperations.OfType<IProductTax>().ApplyPriceOperation(product);

			decimal regularDiscountSum =
					priceModifiers
					.ProductOperations
					.OfType<IDiscount>()
					.Where(po => po.GetType().GetInterface(nameof(IPrecedenceDiscount)) == null)
					.WithDiscountCalculationStrategy(priceModifiers.DiscountCalculationMode, product)
					.WithDiscountCap(priceModifiers.DiscountCap, product);

			decimal aditionalExpenses =
				priceModifiers
				.ProductOperations
				.OfType<IExpense>()
				.SumExpenses(product);

			decimal finalPrice = product
					   .Price
					   .Add(taxAmount)
					   .Add(aditionalExpenses)
					   .Substract(regularDiscountSum)
					   .WithPrecision(Constants.DefaultPrecision);

			return new ProductCosts(taxAmount, regularDiscountSum, aditionalExpenses, finalPrice);
		}

		PriceCalculationResult BuildPriceCalculationResult(IProduct product, IPriceModifierBuilder priceModifiers, ProductCosts costs)
			=> new PriceCalculationResult()
			   .ForProduct(product)
			   .WithInitialPrice(product.Price)
			   .WithExpenses(priceModifiers.ProductOperations.OfType<IExpense>())
			   .WithTax(costs.Tax)
			   .WithDiscounts(costs.Discounts)
			   .WithTotal(costs.Total)
			   .WithFormat(priceModifiers.CurrencyFormat);
	}
}
