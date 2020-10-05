using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculator.Models
{
	public class PriceCalculationResult : ICalculationResult
	{

		public IProduct product;
		public IFormatProvider currencyFormat;
		public Money Cost { get; set; }
		public Money Tax { get; set; }
		public Money Discounts { get; set; }
		public IEnumerable<IExpense> Expenses { get; set; }
		public decimal Total { get; set; }

		public PriceCalculationResult ForProduct(IProduct product)
		{
			this.product = product;
			return this;
		}

		public PriceCalculationResult WithTax(Money tax)
		{
			Tax = tax.WithPrecision(Constants.DefaultPrecision);
			return this;
		}
		public PriceCalculationResult WithInitialPrice(Money price)
		{
			Cost = price.WithPrecision(Constants.DefaultPrecision);
			return this;
		}
		public PriceCalculationResult WithDiscounts(Money discount)
		{
			Discounts = discount.WithPrecision(Constants.DefaultPrecision);
			return this;
		}
		public PriceCalculationResult WithExpenses(IEnumerable<IExpense> expenses)
		{
			Expenses = expenses;
			return this;
		}

		public PriceCalculationResult WithTotal(Money total)
		{
			Total = total.WithPrecision(Constants.DefaultPrecision).Amount;
			return this;
		}

		public PriceCalculationResult WithFormat(IFormatProvider currencyFormat)
		{
			this.currencyFormat = currencyFormat;
			return this;
		}

	}
}
