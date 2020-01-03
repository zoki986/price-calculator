using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using StrategyDesignPattern.PriceCalculationStrategies;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace StrategyDesignPattern.Builder
{
	public class PriceModifiersBuilder
	{
		public ITax Tax { get; set; }
		public List<IDiscount> Discounts { get; set; } = new List<IDiscount>();
		public List<IExpense> AdditionalExpenses { get; set; } = new List<IExpense>();
		public IPriceCalculation CalculationStrategy { get; set; } = new AdditivePriceCalculation();

		public DiscountCap Cap;

		public NumberFormatInfo formatInfo;

		public PriceModifiersBuilder WithCurrencyFormat(string simbol, bool suffix = true)
		{
			formatInfo = new NumberFormatInfo();
			if (string.IsNullOrWhiteSpace(simbol) && Regex.IsMatch(simbol, "[A-Z]{3}"))
				simbol = "USD";

			formatInfo.CurrencySymbol = simbol;

			if (suffix)
				formatInfo.CurrencyPositivePattern = 3;

			return this;
		}

		public PriceModifiersBuilder WithTax(ITax tax)
		{
			this.Tax = tax;
			return this;
		}

		public PriceModifiersBuilder WithDiscount(IDiscount discount)
		{
			Discounts.Add(discount);
			return this;
		}

		public PriceModifiersBuilder WithExpense(IExpense expense)
		{
			AdditionalExpenses.Add(expense);
			return this;
		}

		public PriceModifiersBuilder WithAdditiveCalculation()
		{
			this.CalculationStrategy = new AdditivePriceCalculation();
			return this;
		}

		public PriceModifiersBuilder WithCap(decimal cap, ValueType type)
		{
			this.Cap = new DiscountCap(cap, type);
			return this;
		}

		public PriceModifiersBuilder WithMultiplicativeCalculation()
		{
			this.CalculationStrategy = new MultiplicativeCalculation();
			return this;
		}

		public void Reset()
		{
			Tax = null;
			Discounts = new List<IDiscount>();
			AdditionalExpenses = new List<IExpense>();
			Cap = null;
			CalculationStrategy = null;
		}
	}

}
