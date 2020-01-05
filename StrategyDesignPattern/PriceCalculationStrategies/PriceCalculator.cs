using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Common;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using System.Linq;

namespace StrategyDesignPattern.PriceCalculationStrategies
{
	//public class PriceCalculator : IPriceCalculation
	//{
	//	public PriceCalculationResult GetPriceResultForProduct(IProduct product, PriceModifiersBuilder priceModifiers)
	//	{
	//		//var discountCalculationStrategy = priceModifiers.DiscountCalculationMode;

	//		//var additionlExpenses = priceModifiers.AdditionalExpenses.SumExpenses(product);

	//		//var productWithNewPrice = priceModifiers.Discounts.ApplyPrecedence(product);

	//		//var taxAmount = priceModifiers.Tax.ApllyPriceModifier(productWithNewPrice);

	//		//var discountAmount = priceModifiers
	//		//					.Discounts
	//		//					.Where(discount => !discount.HasPrecedence)
	//		//					.WithDiscountCalculationStrategy(discountCalculationStrategy, productWithNewPrice)
	//		//					.WithDiscountCap(priceModifiers.Cap, product);

	//		//var total = productWithNewPrice
	//		//			.Price
	//		//			.SumWith(taxAmount)
	//		//			.SumWith(additionlExpenses)
	//		//			.Substract(discountAmount)
	//		//			.WithPrecision(Constants.DefaultPrecision);

	//		//return new PriceCalculationResult()
	//		//			.ForProduct(product)
	//		//			.WithInitialPrice(product.Price)
	//		//			.WithExpenses(priceModifiers.AdditionalExpenses)
	//		//			.WithTax(taxAmount)
	//		//			.WithDiscounts(discountAmount)
	//		//			.WithTotal(total)
	//		//			.WithFormat(priceModifiers.CurrencyFormat);

	//		return new ConcreteCalculator(priceModifiers, product).GetPriceResultForProduct();
	//	}
	//}

	public abstract class AbstractCalculator
	{
		protected abstract IMoney ApplyTax();
		protected abstract IMoney SumAdditionalExpenses();
		protected abstract IProduct ApplyPrecedenceDiscount();
		protected abstract IMoney ApplyRegularDiscounts();
		protected abstract IMoney CalculateTotal(Cost costs);
		protected virtual void BuildPriceCalculationResult(Cost costs, IMoney total) { }
	}

	public class PriceCalculator : AbstractCalculator, IPriceCalculation
	{
		private IPriceModifierBuilder _priceModifiers;
		private IProduct _product;
		private IProduct _productWithPrecedenceDiscount;
		private PriceCalculationResult _priceCalculationResult;

		public PriceCalculationResult GetPriceResultForProduct(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			SetProductAndModifiers(product, priceModifiers);

			_productWithPrecedenceDiscount = ApplyPrecedenceDiscount();

			var costs = CalculateCosts();

			var total = CalculateTotal(costs);

			BuildPriceCalculationResult(costs, total);

			return _priceCalculationResult;
		}
		private Cost CalculateCosts()
		{
			return new Cost(ApplyTax(), ApplyRegularDiscounts(), SumAdditionalExpenses());
		}
		private void SetProductAndModifiers(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			_priceModifiers = priceModifiers;
			_product = product;
		}
		protected override void BuildPriceCalculationResult(Cost costs, IMoney total)
		{
			_priceCalculationResult = new PriceCalculationResult()
						.ForProduct(_product)
						.WithInitialPrice(_product.Price)
						.WithExpenses(_priceModifiers.AdditionalExpenses)
						.WithTax(costs.Tax)
						.WithDiscounts(costs.Discounts)
						.WithTotal(total)
						.WithFormat(_priceModifiers.CurrencyFormat);
		}
		protected override IMoney SumAdditionalExpenses()
		{
			return _priceModifiers.AdditionalExpenses.SumExpenses(_product);
		}
		protected override IMoney ApplyTax()
		{
			return _priceModifiers.Tax.ApllyPriceModifier(this._productWithPrecedenceDiscount);
		}
		protected override IProduct ApplyPrecedenceDiscount()
		{
			return _priceModifiers.Discounts.ApplyPrecedence(_product);
		}
		protected override IMoney ApplyRegularDiscounts()
		{
			return _priceModifiers
								.Discounts
								.Where(discount => !discount.HasPrecedence)
								.WithDiscountCalculationStrategy(_priceModifiers.DiscountCalculationMode, _productWithPrecedenceDiscount)
								.WithDiscountCap(_priceModifiers.Cap, _product);
		}
		protected override IMoney CalculateTotal(Cost costs)
		{
			return _productWithPrecedenceDiscount.Price.SumWith(costs.Tax).SumWith(costs.Expenses).Substract(costs.Discounts).WithPrecision(Constants.DefaultPrecision);
		}
	}

	public class Cost
	{
		public Cost(IMoney tax, IMoney discounts, IMoney expenses)
		{
			Tax = tax;
			Discounts = discounts;
			Expenses = expenses;
		}

		public IMoney Tax { get; }
		public IMoney Discounts { get; }
		public IMoney Expenses { get; }
	}
}
