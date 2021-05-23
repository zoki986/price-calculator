using Newtonsoft.Json;
using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceCalculationStrategies;
using PriceCalculator.PriceModifiersModels;
using PriceCalculatorTests;
using System.Globalization;
using Xunit;

namespace Tests
{
	public class PriceCalculatorTests
	{
		IProduct product = PriceDependencies.GetSimpleProduct("The Little Prince", 12345, 20.25m);
		IProduct productGbp = PriceDependencies.GetSimpleProduct("The Little Prince", 12345, 17.76m);

		IProductTax taxPercent20 = PriceDependencies.GetTaxWithAmount(.20M);
		IProductTax taxPercent21 = PriceDependencies.GetTaxWithAmount(.21M);

		IDiscount universalDiscount = new Discount().WithDiscount(.15M);
		IDiscount specialDiscountSpecificForProduct = new SpecialUPCDiscount().WithDiscount(.07M).WithUPC(12345);
		IDiscount nonSpecialDiscount = new SpecialUPCDiscount().WithDiscount(.07M).WithUPC(789);
		IPriceModifier specialDiscountWithPrecedence = new PrecedenceDiscount().WithDiscount(.07M).WithUPC(12345);

		IExpense transport = PriceDependencies.GetExpense("Transport", new MonetaryCost(2.2m));
		IExpense packaging = PriceDependencies.GetExpense("Packaging", new PercentageCost(.01M));

		IPriceCalculation priceCalculator = new CalculationStrategy();
		ProductModifiersBuilder priceModifiersBuilder = new ProductModifiersBuilder();

		[Fact]
		public void Product_Price_With_Tax20_Percent()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = new Money(24.30M);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Product_Price_With_Tax21_Percent()
		{
			priceModifiersBuilder
			.WithTax(taxPercent21);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = new Money(24.50M);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Product_Price_With_General_Discount()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20)
			.WithDiscount(universalDiscount);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = new Money(21.26M);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Product_Price_With_Special_Discount_20PercentTax_ShouldBeApplied()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20)
			.WithDiscount(universalDiscount)
			.WithDiscount(specialDiscountSpecificForProduct);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expectedPrice = new Money(19.84M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Special_Discount_21PercentTax_ShouldNotBeApplied()
		{
			priceModifiersBuilder
			.WithTax(taxPercent21)
			.WithDiscount(universalDiscount)
			.WithDiscount(nonSpecialDiscount);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expectedPrice = new Money(21.46M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Precedence_DiscountApplied()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20)
			.WithDiscount(universalDiscount)
			.WithDiscount(specialDiscountWithPrecedence);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expectedPrice = new Money(19.78M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Additional_Expenses_Added()
		{
			priceModifiersBuilder
				.WithDiscount(universalDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscountSpecificForProduct)
				.WithExpense(packaging)
				.WithExpense(transport)
				.WithAdditiveCalculation()
				.WithCurrencyFormat(CultureInfo.CurrentCulture.NumberFormat);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);
			var t = JsonConvert.SerializeObject(priceModifiersBuilder);

			var expectedPrice = new Money(22.44M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_NoAdditional_Expenses_Or_Discounts()
		{
			priceModifiersBuilder
				.WithTax(taxPercent21);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);
			var expectedPrice = new Money(24.5M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Multiplicative_Discount_Calculation()
		{
			priceModifiersBuilder = new ProductModifiersBuilder()
				.WithDiscount(universalDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscountSpecificForProduct)
				.WithExpense(packaging)
				.WithExpense(transport)
				.WithMultiplicativeCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expectedPrice = new Money(22.66M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Additive_Discount_Calculation()
		{
			priceModifiersBuilder = new ProductModifiersBuilder()
				.WithDiscount(universalDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscountSpecificForProduct)
				.WithExpense(packaging)
				.WithExpense(transport)
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expectedPrice = new Money(22.44M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Max_Cap_Percentage_Of_Price()
		{
			priceModifiersBuilder
				.WithDiscount(universalDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscountSpecificForProduct)
				.WithCap(new PercentageCost(.20M))
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expectedPrice = new Money(20.45M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Max_Cap_Absolute_Value()
		{
			priceModifiersBuilder
				.WithDiscount(universalDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscountSpecificForProduct)
				.WithCap(new MonetaryCost(4M))
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expectedPrice = new Money(20.50M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Custom_Currency_Format()
		{
			var currencyFormat = new NumberFormatInfo { CurrencySymbol = "DIN", CurrencyPositivePattern = 3 };

			priceModifiersBuilder
				.WithDiscount(universalDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscountSpecificForProduct)
				.WithCap(new MonetaryCost(4M))
				.WithAdditiveCalculation()
				.WithCurrencyFormat(currencyFormat);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);
			var actual = priceResult.Total.ToString();
			var expectedFormt = "20.50 DIN";

			Assert.Equal(expectedFormt, actual);
		}

		[Fact]
		public void Product_Price_With_Additional_Precision()
		{
			var transport = PriceDependencies.GetExpense("Transport", new PercentageCost(0.03M));

			priceModifiersBuilder = new ProductModifiersBuilder()
			   .WithDiscount(universalDiscount)
			   .WithTax(taxPercent21)
			   .WithExpense(transport)
			   .WithDiscount(specialDiscountSpecificForProduct)
			   .WithMultiplicativeCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expectedPrice = new Money(20.87M);
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PRECISION_Report_Printed()
		{
			var transport = PriceDependencies.GetExpense("Transport", new PercentageCost(0.03M));
			var currencyFormat = new NumberFormatInfo { CurrencySymbol = "USD", CurrencyPositivePattern = 3 };

			priceModifiersBuilder = new ProductModifiersBuilder()
			   .WithDiscount(universalDiscount)
			   .WithTax(taxPercent21)
			   .WithExpense(transport)
			   .WithDiscount(specialDiscountSpecificForProduct)
			   .WithMultiplicativeCalculation()
			   .WithCurrencyFormat(currencyFormat);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expected = @"Cost = 20.25 USD " +
							"Tax = 4.25 USD " +
							"Discounts = 4.24 USD " +
							"Transport = 0.61 USD " +
							"TOTAL = 20.87 USD";

			var actual = priceResult.ToString().CleanseString();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void CURRENCY_Report_Printed()
		{
			var currencyFormatGbp = new NumberFormatInfo { CurrencySymbol = "GBP", CurrencyPositivePattern = 3 };

			priceModifiersBuilder = new ProductModifiersBuilder()
			   .WithTax(taxPercent20)
			   .WithCurrencyFormat(currencyFormatGbp);

			var priceResult = priceCalculator.GetPriceResultForProduct(productGbp, priceModifiersBuilder);

			var expected = @"Cost = 17.76 GBP " +
							"Tax = 3.55 GBP " +
							"TOTAL = 21.31 GBP";

			var actual = priceResult.ToString().CleanseString();
			
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void COMBINING_Report_Printed()
		{
			priceModifiersBuilder = new ProductModifiersBuilder()
			   .WithTax(taxPercent21)
			   .WithDiscount(universalDiscount)
			   .WithDiscount(specialDiscountSpecificForProduct)
			   .WithMultiplicativeCalculation()
			   .WithExpense(packaging, transport);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var expected = @"Cost = $20.25 " +
							"Tax = $4.25 " +
							"Discounts = $4.24 " +
							"Packaging = $0.20 " +
							"Transport = $2.2 " +
							"TOTAL = $22.66";

			var actual = priceResult.ToString().CleanseString();

			Assert.Equal(expected, actual);
		}
	}
}
