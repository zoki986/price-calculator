using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Common;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using StrategyDesignPattern.PriceCalculationStrategies;
using StrategyDesignPattern.PriceModifiers;
using StrategyDesignPattern.Strategies;
using System;
using ValueType = StrategyDesignPattern.Models.ValueType;

namespace StrategyDesignPattern
{
	class Program
	{
		static void Main(string[] args)
		{
			IProduct product = new Book("The Little Prince", 12345, new Money(20.25M));

			var tax = new TaxPriceModifier(new Money(.20M));
			var discount = new Discount().WithDiscount(new Money(.15M));
			var specialDiscount = new SpecialDiscount().WithDiscount(new Money(.07M)).WithUPC(12345);

			var priceModifiersBuilder = new
			PriceModifiersBuilder()
			.WithTax(tax);

			PriceCalculationContext context = new PriceCalculationContext(priceModifiersBuilder);
			context.CalculateAndReportPrice(product);
			priceModifiersBuilder.WithTax(tax).WithDiscount(discount);

			context.SetModifiers(priceModifiersBuilder);
			context.CalculateAndReportPrice(product);

			priceModifiersBuilder.WithDiscount(specialDiscount);
			context.SetModifiers(priceModifiersBuilder);
			context.CalculateAndReportPrice(product);

			tax = new TaxPriceModifier(new Money(.21M))
			.WithPrecision(Constants.MoneyRelatedPrecision);

			IDiscount relativeDiscount = new Discount()
							.WithDiscount(new Money(.15M))
							.WithPrecision(Constants.MoneyRelatedPrecision);

			 specialDiscount = new SpecialDiscount()
							.WithDiscount(new Money(.07M))
							.WithUPC(12345)
							.WithPrecision(Constants.MoneyRelatedPrecision);

			var transport = new Expense("Transport", 2.2M, ValueType.Monetary);
			var packaging = new Expense("Packaging", .01M, ValueType.Percentage);

			priceModifiersBuilder = new PriceModifiersBuilder()
				.WithConfigurationFile(@"Config/config.txt");

			context = new PriceCalculationContext();
			context.SetModifiers(priceModifiersBuilder);
			context.CalculateAndReportPrice(product);

			Console.ReadLine();
		}
	}
}
