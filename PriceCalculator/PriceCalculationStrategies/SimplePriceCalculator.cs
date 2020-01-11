using PriceCalculator.Builder;
using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System.Linq;

namespace PriceCalculator.PriceCalculationStrategies
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

	public class SimplePriceCalculator : IPriceCalculation
	{
		private IPriceModifierBuilder _priceModifiers;
		private IProduct _product;
		private IProduct _productWithPrecedenceDiscount;

		public PriceCalculationResult GetPriceResultForProduct(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			SetProductAndModifiers(product, priceModifiers);

			_productWithPrecedenceDiscount = ApplyPrecedenceDiscount();

			var costs = CalculateCosts();

			var total = CalculateTotal(costs);

			return BuildPriceCalculationResult(costs, total);
		}

		Cost CalculateCosts()
		{
			return new Cost(ApplyTax(), ApplyRegularDiscounts(), SumAdditionalExpenses());
		}
		void SetProductAndModifiers(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			_priceModifiers = priceModifiers;
			_product = product;
		}
		PriceCalculationResult BuildPriceCalculationResult(Cost costs, decimal total)
		{
		 return new PriceCalculationResult()
						.ForProduct(_product)
						.WithInitialPrice(_product.Price)
						.WithExpenses(_priceModifiers.AdditionalExpenses)
						.WithTax(costs.Tax)
						.WithDiscounts(costs.Discounts)
						.WithTotal(total)
						.WithFormat(_priceModifiers.CurrencyFormat);
		}
		decimal SumAdditionalExpenses()
		{
			return _priceModifiers.AdditionalExpenses.SumExpenses(_product);
		}
		decimal ApplyTax()
		{
			return _priceModifiers.Tax.ApllyPriceModifier(this._productWithPrecedenceDiscount);
		}
		IProduct ApplyPrecedenceDiscount()
		{
			return _priceModifiers.Discounts.ApplyPrecedence(_product,_priceModifiers);
		}
		decimal ApplyRegularDiscounts()
		{
			return _priceModifiers
								.Discounts
								.Where(discount => !discount.HasPrecedence)
								.WithDiscountCalculationStrategy(_priceModifiers.DiscountCalculationMode, _productWithPrecedenceDiscount)
								.WithDiscountCap(_priceModifiers.DiscountCap, _product);
		}
		decimal CalculateTotal(Cost costs)
		{
			return _productWithPrecedenceDiscount.Price.SumWith(costs.Tax).SumWith(costs.Expenses).Substract(costs.Discounts).WithPrecision(Constants.DefaultPrecision);
		}
	}
}
