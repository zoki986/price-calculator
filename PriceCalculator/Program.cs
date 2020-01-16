using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceCalculationStrategies;
using PriceCalculator.PriceModifiers;
using PriceCalculator.Reporters;
using System;

namespace PriceCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			IProduct product = new Book("The Little Prince", 12345, 20.25M);

			var tax = new TaxPriceModifier(.20M);
			var discount = new Discount().WithDiscount(.15M);
			var specialDiscount = new SpecialDiscount().WithDiscount(.07M).WithUPC(12345);

			IPriceCalculation priceCalculation = new CalculationStrategy();
			var priceModifiers = new Builder.PriceModifiers().WithTax(tax).WithDiscount(discount).WithDiscount(specialDiscount);

			var result = priceCalculation.GetPriceResultForProduct(product, priceModifiers);

			ConsoleReporter.PrintResult(result);

			Console.ReadLine();
		}
	}
}
