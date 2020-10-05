using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.PriceModifiersModels
{
	public class DiscountCap
	{
		public IExpenseType CapType { get; }

		public DiscountCap(IExpenseType type)
		{
			CapType = type;
		}
		public decimal GetMaxDiscount(decimal discount, IProduct product)
		{
			var discountCap = GetDiscountCap(product);
			return discount >= discountCap ? discountCap : discount;
		}
		private decimal GetDiscountCap(IProduct product)
			=> CapType.GetCostAmount(product.Price.Amount);
	}
}
