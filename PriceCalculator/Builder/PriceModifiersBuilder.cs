using PriceCalculator.Interfaces;
using PriceCalculator.PriceModifiers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using static PriceCalculator.PriceCalculationStrategies.PriceCalculationFunctions;
using CostType = PriceCalculator.Models.CostType;

namespace PriceCalculator.Builder
{
	public class PriceModifiersBuilder : IPriceModifierBuilder
	{
		public ITax Tax { get; set; }
		public List<IDiscount> Discounts { get; set; } = new List<IDiscount>();
		public List<IExpense> AdditionalExpenses { get; set; } = new List<IExpense>();
		public NumberFormatInfo CurrencyFormat { get; set; } = new NumberFormatInfo() { CurrencySymbol = "$" };
		public Func<IEnumerable<IDiscount>, IProduct, decimal> DiscountCalculationMode { get; set; } = SumDiscounts;
		public DiscountCap DiscountCap { get; set; }

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

		public PriceModifiersBuilder WithTax(ITax tax)
		{
			this.Tax = tax;
			return this;
		}

		public PriceModifiersBuilder WithDiscount(params IDiscount[] discount)
		{
			Discounts.AddRange(discount);
			return this;
		}

		public PriceModifiersBuilder WithExpense(params IExpense[] expense)
		{
			AdditionalExpenses.AddRange(expense);
			return this;
		}

		public PriceModifiersBuilder WithAdditiveCalculation()
		{
			this.DiscountCalculationMode = SumDiscounts;
			return this;
		}

		public PriceModifiersBuilder WithCap(decimal cap, CostType type)
		{
			this.DiscountCap = new DiscountCap(cap, type);
			return this;
		}

		public PriceModifiersBuilder WithMultiplicativeCalculation()
		{
			DiscountCalculationMode = MultypliDiscounts;
			return this;
		}

		public PriceModifiersBuilder WithConfigurationFile(string filePath)
		{
			return PriceModifierBuilderFromConfig.GetPriceModifierBuilder(filePath);
		}

		public void Reset()
		{
			Tax = null;
			Discounts = new List<IDiscount>();
			AdditionalExpenses = new List<IExpense>();
			DiscountCap = null;
		}
	}

}
