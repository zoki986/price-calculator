using PriceCalculator.Builder;
using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiers;
using PriceCalculator.Strategies;
using System;
using ValueType = PriceCalculator.Models.ValueType;

namespace PriceCalculator
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

			_ = new TaxPriceModifier(new Money(.21M))
			.WithPrecision(Constants.MoneyRelatedPrecision);
			_ = new Discount()
							.WithDiscount(new Money(.15M))
							.WithPrecision(Constants.MoneyRelatedPrecision);

			_ = new SpecialDiscount()
							.WithDiscount(new Money(.07M))
							.WithUPC(12345)
							.WithPrecision(Constants.MoneyRelatedPrecision);
			_ = new Expense("Transport", 2.2M, ValueType.Monetary);
			_ = new Expense("Packaging", .01M, ValueType.Percentage);

			priceModifiersBuilder = new PriceModifiersBuilder()
				.WithConfigurationFile(@"Config/config.txt");

			context = new PriceCalculationContext();
			context.SetModifiers(priceModifiersBuilder);
			context.CalculateAndReportPrice(product);

			Console.ReadLine();
		}
	}
}
