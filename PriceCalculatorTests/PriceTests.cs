using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceCalculationStrategies;
using PriceCalculator.PriceModifiers;
using PriceCalculator.Common;
using Xunit;

namespace Tests
{
	public class PriceTests
	{
		IProduct product = PriceDependencies.GetSimpleProduct();
		
		IProductTax taxPercent20 = PriceDependencies.GetTaxWithAmount(.20M);
		IProductTax taxPercent21 = PriceDependencies.GetTaxWithAmount(.21M);

		IDiscount universalDiscount = new Discount().WithDiscount(.15M);
		IDiscount specialDiscountSpecificForProduct = new SpecialUPCDiscount().WithDiscount(.07M).WithUPC(12345);
		IDiscount nonSpecialDiscount = new SpecialUPCDiscount().WithDiscount(.07M).WithUPC(789);
		IPriceModifier specialDiscountWithPrecedence = new PrecedenceDiscount().WithDiscount(.07M).WithUPC(12345);

		IExpense transport = PriceDependencies.GetExpense("Transport", 2.2m, new MonetaryCost());
		IExpense packaging = PriceDependencies.GetExpense("Packaging", .01M, new PercentageCost());

		IPriceCalculation priceCalculator = new CalculationStrategy();
		PriceModifiersBuilder priceModifiersBuilder = new PriceModifiersBuilder();

		[Fact]
		public void Product_Price_With_Tax20_Percent()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = 24.30M;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Product_Price_With_Tax21_Percent()
		{
			priceModifiersBuilder
			.WithTax(taxPercent21);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = 24.50M;

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
			var expected = 21.26M;

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

			decimal expectedPrice = 19.84M;
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

			decimal expectedPrice = 21.46M;
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

			decimal expectedPrice = 19.78M;
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
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 22.44M;
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_NoAdditional_Expenses_Or_Discounts()
		{
			priceModifiersBuilder
				.WithTax(taxPercent21);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);
			decimal expectedPrice = 24.5M;
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Multiplicative_Discount_Calculation()
		{
			priceModifiersBuilder = new PriceModifiersBuilder()
				.WithDiscount(universalDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscountSpecificForProduct)
				.WithExpense(packaging)
				.WithExpense(transport)
				.WithMultiplicativeCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 22.66M;
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Additive_Discount_Calculation()
		{
			priceModifiersBuilder = new PriceModifiersBuilder()
				.WithDiscount(universalDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscountSpecificForProduct)
				.WithExpense(packaging)
				.WithExpense(transport)
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 22.44M;
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
				.WithCap(.20M, new PercentageCost())
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.45M;
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
				.WithCap(4M, new MonetaryCost())
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.50M;
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Custom_Currency_Format()
		{
			priceModifiersBuilder
				.WithDiscount(universalDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscountSpecificForProduct)
				.WithCap(4M, new MonetaryCost())
				.WithAdditiveCalculation()
				.WithCurrencyFormat("USD");

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);
			string actual = priceResult.Total.FormatDecimal(priceResult.currencyFormat);
			string expectedFormt = "20.50 USD";

			Assert.Equal(expectedFormt, actual);
		}

		[Fact]
		public void Product_Price_With_Additional_Precision()
		{
			var transport = PriceDependencies.GetExpense("Transport", 0.03M, new PercentageCost());

			priceModifiersBuilder = new PriceModifiersBuilder()
			   .WithDiscount(universalDiscount)
			   .WithTax(taxPercent21)
			   .WithExpense(transport)
			   .WithDiscount(specialDiscountSpecificForProduct)
			   .WithMultiplicativeCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.87M;
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Configuration_File()
		{

			priceModifiersBuilder = new PriceModifiersBuilder()
			.WithConfigurationFile(@"Config/config.txt");

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.87M;
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

	}
}
