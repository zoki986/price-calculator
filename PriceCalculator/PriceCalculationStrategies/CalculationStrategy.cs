using PriceCalculator.Builder;
using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
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
			IProduct precedenceDiscountProduct = 
				priceModifiers
				.Discounts
				.Where(discount => discount.HasPrecedence)
				.ApplyPrecedence(product, priceModifiers);

			IEnumerable<IDiscount> regularDiscounts = priceModifiers.Discounts.Where(discount => !discount.HasPrecedence);

			decimal tax = priceModifiers.Tax.ApllyPriceModifier(precedenceDiscountProduct);

			decimal regularDiscountSum =
					regularDiscounts
					.WithDiscountCalculationStrategy(priceModifiers.DiscountCalculationMode, precedenceDiscountProduct)
					.WithDiscountCap(priceModifiers.DiscountCap, product);

			decimal aditionalExpenses =
				priceModifiers
				.AdditionalExpenses
				.SumExpenses(product);

			decimal finalPrice = precedenceDiscountProduct
					   .Price
					   .SumWith(tax)
					   .SumWith(aditionalExpenses)
					   .Substract(regularDiscountSum)
					   .WithPrecision(Constants.DefaultPrecision);

			return new ProductCosts(tax, regularDiscountSum, aditionalExpenses, finalPrice);
		}

		PriceCalculationResult BuildPriceCalculationResult(IProduct product, IPriceModifierBuilder priceModifiers, ProductCosts costs)
			=> new PriceCalculationResult()
			   .ForProduct(product)
			   .WithInitialPrice(product.Price)
			   .WithExpenses(priceModifiers.AdditionalExpenses)
			   .WithTax(costs.Tax)
			   .WithDiscounts(costs.Discounts)
			   .WithTotal(costs.Total)
			   .WithFormat(priceModifiers.CurrencyFormat);
	}
}
