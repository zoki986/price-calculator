using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceCalculationStrategies;
using System;

namespace PriceCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			IProduct product = new Book("The Little Prince", 12345, new Money(20.25M));

			//var tax = new TaxPriceModifier(.20M);
			//var discount = new Discount().WithDiscount(.15M);
			///var specialDiscount = new SpecialUPCDiscount().WithDiscount(.07M).WithUPC(12345);

			IPriceCalculation priceCalculation = new CalculationStrategy();
			//var priceModifiers = new PriceModifiersBuilder().WithTax(tax).WithDiscount(discount).WithDiscount(specialDiscount);

			// result = priceCalculation.GetPriceResultForProduct(product, priceModifiers);

			Console.ReadLine();
		}
	}
}
