using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceModifiers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using static PriceCalculator.PriceCalculationStrategies.PriceCalculationFunctions;

namespace PriceCalculator.Builder
{
	public class PriceModifiersBuilder : IPriceModifierBuilder
	{
		public List<IPriceModifier> ProductPriceModifiers { get; set; } = new List<IPriceModifier>();
		public NumberFormatInfo CurrencyFormat { get; set; } = new NumberFormatInfo() { CurrencySymbol = "$" };
		public Func<IEnumerable<IDiscount>, IProduct, decimal> DiscountCalculationMode { get; set; } = SumDiscounts;
		public DiscountCap DiscountCap { get; set; }

		public int CalculationPrecision { get; set; } = 2;
		public int ReportPrecision { get; set; } = 2;

		public PriceModifiersBuilder()
		{
		}

		public PriceModifiersBuilder WithCurrencyFormat(string simbol, bool suffix = true)
		{
			CurrencyFormat = new NumberFormatInfo();
			if (string.IsNullOrWhiteSpace(simbol) && !Regex.IsMatch(simbol, "[A-Z]{3}"))
				simbol = "USD";

			CurrencyFormat.CurrencySymbol = simbol;
			CurrencyFormat.CurrencyPositivePattern = 2;

			if (suffix)
				CurrencyFormat.CurrencyPositivePattern = 3;

			return this;
		}

		public PriceModifiersBuilder WithTax(IProductTax tax)
		{
			ProductPriceModifiers.Add(tax);
			return this;
		}

		public PriceModifiersBuilder WithDiscount(params IPriceModifier[] discount)
		{
			ProductPriceModifiers.AddRange(discount);
			return this;
		}

		public PriceModifiersBuilder WithExpense(params IExpense[] expense)
		{
			ProductPriceModifiers.AddRange(expense);
			return this;
		}

		public PriceModifiersBuilder WithAdditiveCalculation()
		{
			this.DiscountCalculationMode = SumDiscounts;
			return this;
		}

		public PriceModifiersBuilder WithCap(IExpenseType type)
		{
			this.DiscountCap = new DiscountCap(type);
			return this;
		}

		public PriceModifiersBuilder WithMultiplicativeCalculation()
		{
			DiscountCalculationMode = MultypliDiscounts;
			return this;
		}

		public PriceModifiersBuilder WithPriceModifiers(IEnumerable<IPriceModifier> priceModifiers)
		{
			ProductPriceModifiers.AddRange(priceModifiers);
			return this;
		}

		public PriceModifiersBuilder WithConfigurationFile(string filePath)
		{
			return PriceModifierBuilderFromConfig.GetPriceModifierBuilder(filePath);
		}
	}

}
