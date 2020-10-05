namespace PriceCalculator.DiscountConditions
{
	public interface IDiscountCondition
	{
		decimal GetConditionResult(decimal price, decimal discountAmount);
	}
}
