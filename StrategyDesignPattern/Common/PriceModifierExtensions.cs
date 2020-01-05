using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.Models;
using System.Linq;

namespace StrategyDesignPattern.Common
{
	public static class PriceModifierExtensions
	{
		public static Result ForProduct(this PriceModifiersBuilder modifiers, IProduct product)
		{
			return new Result(product, product.Price, modifiers);
		}
		public static Result CalculateAdditionalExpenses(this Result result)
		{
			IMoney priceWithAdditionalExpenses = result.Amount.SumWith(result.Modifiers.AdditionalExpenses.SumExpenses(result.Product));
			return new Result(result.Product, priceWithAdditionalExpenses, result.Modifiers);
		}
		public static Result ApplyPrecedenceDiscount(this Result result)
		{
			var discountAppliedProduct = result.Modifiers.Discounts.ApplyPrecedence(result.Product);
			return new Result(result.Product, discountAppliedProduct.Price, result.Modifiers, discountAppliedProduct);
		}

		public static Result ApplyTax(this Result result)
		{
			IMoney withTax = result.Amount.SumWith(result.Modifiers.Tax.ApllyPriceModifier(result.PrecedenceDiscountProduct));
			return new Result(result.Product, withTax, result.Modifiers, result.PrecedenceDiscountProduct);
		}

		public static Result ApplyDiscounts(this Result result)
		{
			var discounts = result.Modifiers.Discounts
							.Where(discount => !discount.HasPrecedence)
							.WithDiscountCalculationStrategy(result.Modifiers.DiscountCalculationMode, result.PrecedenceDiscountProduct)
							.WithDiscountCap(result.Modifiers.Cap, result.Product);

			var newPrice = result.Amount.Substract(discounts);
			return new Result(result.Product, newPrice, result.Modifiers, result.PrecedenceDiscountProduct);
		}

		public static PriceCalculationResult CalculateTotal(this Result result)
		{
			return new PriceCalculationResult().WithTotal(result.Amount);
		}
	}

	public class Result
	{
		public IProduct Product;
		public IProduct PrecedenceDiscountProduct;
		public IMoney Amount;
		public PriceModifiersBuilder Modifiers;
		public Result(IProduct product, IMoney money, PriceModifiersBuilder modifiers, IProduct precedenceProduct = null)
		{
			Product = product;
			Amount = money;
			Modifiers = modifiers;
			PrecedenceDiscountProduct = precedenceProduct;
		}
	}
}
