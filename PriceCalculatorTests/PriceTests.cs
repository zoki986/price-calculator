using PriceCalculator.Builder;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceCalculationStrategies;
using PriceCalculator.PriceModifiers;
using Xunit;

namespace Tests
{
	public class PriceTests
	{
		IProduct product = PriceDependencies.GetSimpleProduct();
		
		ITax taxPercent20 = PriceDependencies.GetTaxWithAmount(.20M);
		ITax taxPercent21 = PriceDependencies.GetTaxWithAmount(.21M);

		IDiscount universalDiscount = new Discount().WithDiscount(.15M);
		IDiscount specialDiscountSpecificForProduct = new SpecialDiscount().WithDiscount(.07M).WithUPC(12345);
		IDiscount nonSpecialDiscount = new SpecialDiscount().WithDiscount(.07M).WithUPC(789);
		IDiscount specialDiscountWithPrecedence = new SpecialDiscount().WithDiscount(.07M).WithUPC(12345).WithPrecedence();

		IExpense transport = PriceDependencies.GetExpense("Transport", 2.2m, ValueType.Monetary);
		IExpense packaging = PriceDependencies.GetExpense("Packaging", .01M, ValueType.Percentage);

		IPriceCalculation priceCalculator = new SimplePriceCalculator();
		PriceModifiersBuilder priceModifiersBuilder = new PriceModifiersBuilder();

		[Fact]
		public void Product_Price_With_Tax20_Percent()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = new Money(24.30M);

			Assert.Equal(expected.Amount, actual);
		}

		[Fact]
		public void Product_Price_With_Tax21_Percent()
		{
			priceModifiersBuilder
			.WithTax(taxPercent21);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = new Money(24.50M);

			Assert.Equal(expected.Amount, actual);
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

			Assert.Equal(expected.Amount, actual);
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
		public void Product_Price_With_Precedence_DiscountApplied_Test()
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
				.WithCap(.20M, ValueType.Percentage)
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
				.WithCap(4M, ValueType.Monetary)
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.50M;
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void Product_Price_With_Additional_Precision()
		{
			var transport = PriceDependencies.GetExpense("Transport", 0.03M, ValueType.Percentage);

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
