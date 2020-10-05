using PriceCalculator.DiscountConditions;
using PriceCalculator.Interfaces;

namespace PriceCalculator.PriceModifiersModels
{
	public class SpecialUPCDiscount : Discount
	{
		public int UPC { get; set; }
		private IDiscountCondition condition;
		new public SpecialUPCDiscount WithDiscount(decimal discount)
		{
			Amount = discount;
			return this;
		}

		public SpecialUPCDiscount WithUPC(int upc)
		{
			UPC = upc;
			return this;
		}

		public override decimal ApllyModifier(IProduct product)
		{
			condition = new SameUPCCondition(UPC, product.UPC);
			return condition.GetConditionResult(product.Price.Amount, Amount);
		}
	}
}
