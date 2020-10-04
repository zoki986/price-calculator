using PriceCalculator.Interfaces;
using PriceCalculator.PriceCalculationStrategies;
using PriceCalculator.PriceModifiersModels;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PriceCalculator.Builder
{
	public class ModifiersBuilder : IPriceModifierBuilder
	{
		public List<IPriceModifier> ProductPriceModifiers { get; set; } = new List<IPriceModifier>();
		public NumberFormatInfo CurrencyFormat { get; set; } = new NumberFormatInfo() { CurrencySymbol = "$" };
		public IDiscountCalculationMode DiscountCalculationMode { get; set; } = new SumingDiscountCalculation();
		public DiscountCap DiscountCap { get; set; }

		public int CalculationPrecision { get; set; } = 2;
		public int ReportPrecision { get; set; } = 2;

		public ModifiersBuilder()
		{
		}

		public ModifiersBuilder WithCurrencyFormat(string simbol, bool suffix = true)
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

		public ModifiersBuilder WithTax(IProductTax tax)
		{
			ProductPriceModifiers.Add(tax);
			return this;
		}

		public ModifiersBuilder WithDiscount(params IPriceModifier[] discount)
		{
			ProductPriceModifiers.AddRange(discount);
			return this;
		}

		public ModifiersBuilder WithExpense(params IExpense[] expense)
		{
			ProductPriceModifiers.AddRange(expense);
			return this;
		}

		public ModifiersBuilder WithAdditiveCalculation()
		{
			this.DiscountCalculationMode = new SumingDiscountCalculation();
			return this;
		}

		public ModifiersBuilder WithCap(IExpenseType type)
		{
			this.DiscountCap = new DiscountCap(type);
			return this;
		}

		public ModifiersBuilder WithMultiplicativeCalculation()
		{
			DiscountCalculationMode = new MultiplicativeDiscountCalculation();
			return this;
		}

		public ModifiersBuilder WithPriceModifiers(IEnumerable<IPriceModifier> priceModifiers)
		{
			ProductPriceModifiers.AddRange(priceModifiers);
			return this;
		}

		public ModifiersBuilder WithConfigurationFile(string filePath)
		{
			return ModifierBuilderFromConfig.GetPriceModifierBuilder(filePath);
		}
	}

}
