using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using StrategyDesignPattern.PriceCalculationStrategies;
using StrategyDesignPattern.PriceModifiers;
using StrategyDesignPattern.Strategies;
using Xunit;

namespace Tests
{
	public class PriceTests
	{
		IProduct product = PriceDependencies.GetSimpleProduct();
		
		ITax taxPercent20 = PriceDependencies.GetTaxWithAmount(.20M);
		ITax taxPercent21 = PriceDependencies.GetTaxWithAmount(.21M);

		IDiscount relativeDiscount = new Discount().WithDiscount(new Money(.15M));
		IDiscount specialDiscount = new SpecialDiscount().WithDiscount(new Money(.07M)).WithUPC(12345);
		IDiscount specialDiscountWithPrecedence = new SpecialDiscount().WithDiscount(new Money(.07M)).WithUPC(12345).WithPrecedence();

		IExpense transport = PriceDependencies.GetExpense("Transport", 2.2m, ValueType.Monetary);
		IExpense packaging = PriceDependencies.GetExpense("Packaging", .01M, ValueType.Percentage);

		IPriceCalculation priceCalculator = new PriceCalculator();
		PriceModifiersBuilder priceModifiersBuilder = new PriceModifiersBuilder();

		[Fact]
		public void PriceWithTax()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = new Money(24.30M);

			Assert.Equal(expected.Amount, actual.Amount);
		}

		[Fact]
		public void PriceWithRelativeDiscount()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20).WithDiscount(relativeDiscount);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = new Money(21.26M);

			Assert.Equal(expected.Amount, actual.Amount);
		}

		[Fact]
		public void PriceWithSpecialDiscount()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20).WithDiscount(relativeDiscount).WithDiscount(specialDiscount);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 19.84M;
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual.Amount);
		}

		[Fact]
		public void PriceWithPrecedenceDiscount()
		{
			priceModifiersBuilder
			.WithTax(taxPercent20).WithDiscount(relativeDiscount).WithDiscount(specialDiscountWithPrecedence);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 19.78M;
			var actual = priceResult.Total.Amount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithAdditionalExpenses()
		{
			priceModifiersBuilder
				.WithDiscount(relativeDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscount)
				.WithExpense(packaging)
				.WithExpense(transport)
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 22.44M;
			var actual = priceResult.Total.Amount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithNoAdditionalExpensesOrDiscounts()
		{
			priceModifiersBuilder
				.WithTax(taxPercent21);

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);
			decimal expectedPrice = 24.5M;
			var actual = priceResult.Total.Amount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithMultiplicativeCalculation()
		{
			priceModifiersBuilder = new PriceModifiersBuilder()
				.WithDiscount(relativeDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscount)
				.WithExpense(packaging)
				.WithExpense(transport)
				.WithMultiplicativeCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 22.66M;
			var actual = priceResult.Total.Amount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithMaxCapPercentage()
		{
			priceModifiersBuilder
				.WithDiscount(relativeDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscount)
				.WithCap(.20M, ValueType.Percentage)
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.45M;
			var actual = priceResult.Total.Amount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithMaxCapAbsolute()
		{
			priceModifiersBuilder
				.WithDiscount(relativeDiscount)
				.WithTax(taxPercent21)
				.WithDiscount(specialDiscount)
				.WithCap(4M, ValueType.Monetary)
				.WithAdditiveCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.50M;
			var actual = priceResult.Total.Amount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithAdditionalPrecision()
		{
			var transport = PriceDependencies.GetExpense("Transport", 0.03M, ValueType.Percentage);

			priceModifiersBuilder = new PriceModifiersBuilder()
			   .WithDiscount(relativeDiscount)
			   .WithTax(taxPercent21)
			   .WithExpense(transport)
			   .WithDiscount(specialDiscount)
			   .WithMultiplicativeCalculation();

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.87M;
			var actual = priceResult.Total.Amount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithConfigFile()
		{

			priceModifiersBuilder = new PriceModifiersBuilder()
			.WithConfigurationFile(@"Config/config.txt");

			var priceResult = priceCalculator.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.87M;
			var actual = priceResult.Total.Amount;

			Assert.Equal(expectedPrice, actual);
		}

	}
}
