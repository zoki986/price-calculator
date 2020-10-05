namespace PriceCalculator.DiscountConditions
{
	public class SameUPCCondition : IDiscountCondition
	{
		private readonly int _upc;
		private readonly int _upcOther;
		private readonly decimal _defaultAmount = 0M;

		public SameUPCCondition(int upc, int upcOther)
		{
			_upc = upc;
			_upcOther = upcOther;
		}

		public bool CanBeApplied()
			=> _upc.Equals(_upcOther);

		public decimal GetConditionResult(decimal price, decimal discountAmount)
			=> CanBeApplied() ? price * discountAmount : _defaultAmount;
	}
}
