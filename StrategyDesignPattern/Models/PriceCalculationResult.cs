using StrategyDesignPattern.Common;
using StrategyDesignPattern.Interfaces;
using System.Collections.Generic;

namespace StrategyDesignPattern.Models
{
	public class PriceCalculationResult
	{
		public PriceCalculationResult WithProduct(IProduct product)
		{
			this.product = product;
			return this;
		}

		public PriceCalculationResult WithTax(IMoney tax)
		{
			Tax = tax.WithPrecision(2);
			return this;
		}
		public PriceCalculationResult WithPriceBefore(IMoney price)
		{
			Cost = price.WithPrecision(2);
			return this;
		}
		public PriceCalculationResult WithDiscounts(IMoney discount)
		{
			Discounts = discount.WithPrecision(2);
			return this;
		}
		public PriceCalculationResult WithExpenses(IEnumerable<IExpense> expenses)
		{
			Expenses = expenses;
			return this;
		}

		public PriceCalculationResult WithTotal(IMoney total)
		{
			Total = total.WithPrecision(2);
			return this;
		}

		public PriceCalculationResult WithFormat(IMoney total)
		{
			Total = total.WithPrecision(2);
			return this;
		}

		public IProduct product;
		public Format format;
		public IMoney Cost { get; set; }
		public IMoney Tax { get; set; }
		public IMoney Discounts { get; set; }
		public IEnumerable<IExpense> Expenses { get; set; }
		public IMoney Total { get; set; }

	}
}
