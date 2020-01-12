using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiers;
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

			Console.ReadLine();
		}
	}
}
