using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class MonetaryCost : IExpenseType
	{
		private readonly decimal _amount;
		private readonly int _decimalsInFormat;
		public int DecimalsInFormat { get => _decimalsInFormat; }

		public MonetaryCost(decimal amount, int decimalsInFormat = 1)
		{
			_amount = amount;
			_decimalsInFormat = decimalsInFormat;
		}


		public decimal GetCostAmount(decimal price)
			=> _amount;
	}
}
