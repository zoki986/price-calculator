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
		IProduct product = new Book("The Little Prince", 12345, new Dolar(20.25M));
		ITax tax = new TaxPriceModifier(new Dolar(.20M));
		IDiscount relativeDiscount = new Discount().WithDiscount(new Dolar(.15M));
		IDiscount specialDiscount = new SpecialDiscount().WithDiscount(new Dolar(.07M)).WithUPC(12345);
		IDiscount specialDiscountWithPrecedence = new SpecialDiscount().WithDiscount(new Dolar(.07M)).WithUPC(12345).WithPrecedence();

		IPriceCalculation pricaCalculatingStrategy = new AdditivePriceCalculation();
		PriceModifiersBuilder priceModifiersBuilder = new PriceModifiersBuilder();

		[Fact]
		public void PriceWithTax()
		{
			priceModifiersBuilder 
			.WithTax(tax);

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = new Dolar(24.30M);

			Assert.Equal(expected.Ammount, actual.Ammount);
		}

		[Fact]
		public void PriceWithRelativeDiscount()
		{
			priceModifiersBuilder
			.WithTax(tax).WithDiscount(relativeDiscount);

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);

			var actual = priceResult.Total;
			var expected = new Dolar(21.26M);

			Assert.Equal(expected.Ammount, actual.Ammount);
		}

		[Fact]
		public void PriceWithSpecialDiscount()
		{
			priceModifiersBuilder 
			.WithTax(tax).WithDiscount(relativeDiscount).WithDiscount(specialDiscount);

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 19.84M;
			var actual = priceResult.Total;

			Assert.Equal(expectedPrice, actual.Ammount);
		}

		[Fact]
		public void PriceWithPrecedenceDiscount()
		{
			priceModifiersBuilder 
			.WithTax(tax).WithDiscount(relativeDiscount).WithDiscount(specialDiscountWithPrecedence);

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 19.78M;
			var actual = priceResult.Total.Ammount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithAdditionalExpenses()
		{
			ITax tax = new TaxPriceModifier(new Dolar(.21M));
			var packaging = new Expense("Transport", 2.2m, ValueType.Monetary);
			var transport = new Expense("Packaging", .01M, ValueType.Percentage);
			priceModifiersBuilder 
				.WithDiscount(relativeDiscount)
				.WithTax(tax)
				.WithDiscount(specialDiscount)
				.WithExpense(packaging)
				.WithExpense(transport)
				.WithAdditiveCalculation();

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 22.44M;
			var actual = priceResult.Total.Ammount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithNoAdditionalExpensesOrDiscounts()
		{
			ITax tax = new TaxPriceModifier(new Dolar(.21M));
			priceModifiersBuilder
				.WithTax(tax);

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);
			decimal expectedPrice = 24.5M;
			var actual = priceResult.Total.Ammount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithMultiplicativeCalculation()
		{
			ITax tax = new TaxPriceModifier(new Dolar(.21M));
			var transport = new Expense("Transport", 2.2M, ValueType.Monetary);
			var packaging = new Expense("Packaging", .01M, ValueType.Percentage);
			priceModifiersBuilder = new PriceModifiersBuilder()
				.WithDiscount(relativeDiscount)
				.WithTax(tax)
				.WithDiscount(specialDiscount)
				.WithExpense(packaging)
				.WithExpense(transport)
				.WithMultiplicativeCalculation();

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 22.44M;
			var actual = priceResult.Total.Ammount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithMaxCapPercentage()
		{
			ITax tax = new TaxPriceModifier(new Dolar(.21M));
			priceModifiersBuilder 
				.WithDiscount(relativeDiscount)
				.WithTax(tax)
				.WithDiscount(specialDiscount)
				.WithCap(.20M, ValueType.Percentage)
				.WithAdditiveCalculation();

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);

			decimal expectedPrice = 20.45M;
			var actual = priceResult.Total.Ammount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithMaxCapAbsolute()
		{
			ITax tax = new TaxPriceModifier(new Dolar(.21M));
			priceModifiersBuilder 
				.WithDiscount(relativeDiscount)
				.WithTax(tax)
				.WithDiscount(specialDiscount)
				.WithCap(4M, ValueType.Monetary)
				.WithAdditiveCalculation();

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);
			decimal expectedPrice = 20.50M;
			var actual = priceResult.Total.Ammount;

			Assert.Equal(expectedPrice, actual);
		}

		[Fact]
		public void PriceWithAdditionalPrecision()
		{
			ITax tax = new TaxPriceModifier(new Dolar(.21M))
				.WithPrecision(4);

			IDiscount relativeDiscount = new Discount()
							.WithDiscount(new Dolar(.15M))
							.WithPrecision(4);

			IDiscount specialDiscount = new SpecialDiscount()
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

			pricaCalculatingStrategy = new MultiplicativeCalculation();

			var priceResult = pricaCalculatingStrategy.GetPriceResultForProduct(product, priceModifiersBuilder);
			decimal expectedPrice = 20.87M;
			var actual = priceResult.Total.Ammount;

			Assert.Equal(expectedPrice, actual);
		}

	}
}
