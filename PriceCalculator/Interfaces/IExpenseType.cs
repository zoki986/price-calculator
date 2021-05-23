using System;

namespace PriceCalculator.Interfaces
{
	public interface IExpenseType
	{
		decimal GetCostAmount(decimal price);
		int DecimalsInFormat { get; }
	}
}
