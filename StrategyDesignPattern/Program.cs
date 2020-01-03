using StrategyDesignPattern.Builder;
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
			IProduct product = new Book("The Little Prince", 12345, new Dolar(20.25M));

			var tax = new TaxPriceModifier(new Dolar(.20M));
			var discount = new Discount().WithDiscount(new Dolar(.15M));
			var specialDiscount = new SpecialDiscount().WithDiscount(new Dolar(.07M)).WithUPC(12345);

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

			 tax = new TaxPriceModifier(new Dolar(.21M))
			.WithPrecision(4);

			IDiscount relativeDiscount = new Discount()
							.WithDiscount(new Dolar(.15M))
							.WithPrecision(4);

			 specialDiscount = new SpecialDiscount()
							.WithDiscount(new Dolar(.07M))
							.WithUPC(12345)
							.WithPrecision(4);

			var transport = new Expense("Transport", 0.03M, ValueType.Percentage);

			priceModifiersBuilder = new PriceModifiersBuilder()
			   .WithDiscount(relativeDiscount)
			   .WithTax(tax)
			   .WithExpense(transport)
			   .WithDiscount(specialDiscount)
			   .WithMultiplicativeCalculation();

			context.SetModifiers(priceModifiersBuilder);
			context.SetStrategy(new MultiplicativeCalculation());
			context.CalculateAndReportPrice(product);

			//var test = BaseCurrencyFormater.FormatCurrencyWithSimbol(expectedPrice,"GBP");
			//Console.WriteLine(test);
			Console.ReadLine();
		}
	}
}
