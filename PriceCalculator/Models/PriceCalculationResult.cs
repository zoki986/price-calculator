using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculator.Models
{
	public class PriceCalculationResult : IResult
	{
		public PriceCalculationResult ForProduct(IProduct product)
		{
			this.product = product;
			return this;
		}

		public PriceCalculationResult WithTax(decimal tax)
		{
			Tax = tax.WithPrecision(Constants.DefaultPrecision);
			return this;
		}
		public PriceCalculationResult WithInitialPrice(decimal price)
		{
			Cost = price.WithPrecision(Constants.DefaultPrecision);
			return this;
		}
		public PriceCalculationResult WithDiscounts(decimal discount)
		{
			Discounts = discount.WithPrecision(Constants.DefaultPrecision);
			return this;
		}
		public PriceCalculationResult WithExpenses(IEnumerable<IExpense> expenses)
		{
			Expenses = expenses;
			return this;
		}

		public PriceCalculationResult WithTotal(decimal total)
		{
			Total = total.WithPrecision(Constants.DefaultPrecision);
			return this;
		}

		public PriceCalculationResult WithFormat(NumberFormatInfo currencyFormat)
		{
			this.currencyFormat = currencyFormat;
			return this;
		}

		public IProduct product;
		public NumberFormatInfo currencyFormat;
		public decimal Cost { get; set; }
		public decimal Tax { get; set; }
		public decimal Discounts { get; set; }
		public IEnumerable<IExpense> Expenses { get; set; }
		public decimal Total { get; set; }

	}
}
