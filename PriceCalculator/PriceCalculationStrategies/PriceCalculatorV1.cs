using PriceCalculator.Builder;
using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using System;
using System.Linq;

namespace PriceCalculator.PriceCalculationStrategies
{
	public class PriceCalculatorV1 : IPriceCalculation
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

		Costs CalculateCosts()
		{
			var tax = ApplyTax();
			var discounts = ApplyRegularDiscounts();
			var expenses = SumAdditionalExpenses();

			return new Costs(tax, discounts, expenses);
		}
		void SetProductAndModifiers(IProduct product, PriceModifiersBuilder priceModifiers)
		{
			_priceModifiers = priceModifiers ?? throw new ArgumentNullException(nameof(priceModifiers));
			_product = product ?? throw new ArgumentNullException(nameof(product));
		}
		PriceCalculationResult BuildPriceCalculationResult(Costs costs, decimal total)
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
			return _priceModifiers.AdditionalExpenses
				   .SumExpenses(_product);
		}
		decimal ApplyTax()
		{
			return _priceModifiers.Tax
				   .ApllyPriceModifier(_productWithPrecedenceDiscount);
		}
		IProduct ApplyPrecedenceDiscount()
		{
			return _priceModifiers
				   .Discounts
				   .ApplyPrecedence(_product, _priceModifiers);
		}
		decimal ApplyRegularDiscounts()
		{
			return _priceModifiers
					.Discounts
					.Where(discount => !discount.HasPrecedence)
					.WithDiscountCalculationStrategy(_priceModifiers.DiscountCalculationMode, _productWithPrecedenceDiscount)
					.WithDiscountCap(_priceModifiers.DiscountCap, _product);
		}
		decimal CalculateTotal(Costs costs)
		{
			return _productWithPrecedenceDiscount
				.Price
				.SumWith(costs.Tax)
				.SumWith(costs.Expenses)
				.Substract(costs.Discounts)
				.WithPrecision(Constants.DefaultPrecision);
		}
	}
}
