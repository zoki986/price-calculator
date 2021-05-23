using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class PercentageCost : IExpenseType
	{
		private readonly decimal _amount;
		private readonly int _decimalsInFormat;
		public int DecimalsInFormat { get => _decimalsInFormat; }

		public PercentageCost(decimal amount, int decimalsInFormat = 2)
		{
			_amount = amount;
			_decimalsInFormat = decimalsInFormat;
		}

		public decimal GetCostAmount(decimal price)
			=> _amount * price;
	}
}
