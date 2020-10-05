using PriceCalculator.Interfaces;
using PriceCalculator.PriceCalculationStrategies;
using PriceCalculator.PriceModifiersModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PriceCalculator.Builder
{
	public class ProductModifiersBuilder : IProductModifiersBuilder
	{
		public List<IPriceModifier> ProductPriceModifiers { get; set; } = new List<IPriceModifier>();
		public IFormatProvider CurrencyFormat { get; set; } = new NumberFormatInfo() { CurrencySymbol = "$" };
		public IDiscountCalculationMode DiscountCalculationMode { get; set; } = new SumingDiscountCalculation();
		public DiscountCap DiscountCap { get; set; }
		public int CalculationPrecision { get; set; } = 2;
		public int ReportPrecision { get; set; } = 2;

		public ProductModifiersBuilder()
		{
		}

		public ProductModifiersBuilder WithCurrencyFormat(IFormatProvider formatProvider)
		{
			CurrencyFormat = formatProvider;
			return this;
		}

		public ProductModifiersBuilder WithTax(IProductTax tax)
		{
			ProductPriceModifiers.Add(tax);
			return this;
		}

		public ProductModifiersBuilder WithDiscount(params IPriceModifier[] discount)
		{
			ProductPriceModifiers.AddRange(discount);
			return this;
		}

		public ProductModifiersBuilder WithExpense(params IExpense[] expense)
		{
			ProductPriceModifiers.AddRange(expense);
			return this;
		}

		public ProductModifiersBuilder WithAdditiveCalculation()
		{
			this.DiscountCalculationMode = new SumingDiscountCalculation();
			return this;
		}

		public ProductModifiersBuilder WithCap(IExpenseType type)
		{
			this.DiscountCap = new DiscountCap(type);
			return this;
		}

		public ProductModifiersBuilder WithMultiplicativeCalculation()
		{
			DiscountCalculationMode = new MultiplicativeDiscountCalculation();
			return this;
		}

		public ProductModifiersBuilder WithPriceModifiers(IEnumerable<IPriceModifier> priceModifiers)
		{
			ProductPriceModifiers.AddRange(priceModifiers);
			return this;
		}

		public ProductModifiersBuilder WithConfigurationFile(string filePath)
		{
			return ProductFileModifierBuilder.GetPriceModifierBuilder(filePath);
		}
	}

}
