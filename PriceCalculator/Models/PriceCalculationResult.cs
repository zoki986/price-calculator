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

		public PriceCalculationResult WithTax(IMoney tax)
		{
			Tax = tax.WithPrecision(Constants.DefaultPrecision);
			return this;
		}
		public PriceCalculationResult WithInitialPrice(IMoney price)
		{
			Cost = price.WithPrecision(Constants.DefaultPrecision);
			return this;
		}
		public PriceCalculationResult WithDiscounts(IMoney discount)
		{
			Discounts = discount.WithPrecision(Constants.DefaultPrecision);
			return this;
		}
		public PriceCalculationResult WithExpenses(IEnumerable<IExpense> expenses)
		{
			Expenses = expenses;
			return this;
		}

		public PriceCalculationResult WithTotal(IMoney total)
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
		public IMoney Cost { get; set; }
		public IMoney Tax { get; set; }
		public IMoney Discounts { get; set; }
		public IEnumerable<IExpense> Expenses { get; set; }
		public IMoney Total { get; set; }

	}
}
