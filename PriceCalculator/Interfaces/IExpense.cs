using PriceCalculator.Models;
using System;

namespace PriceCalculator.Interfaces
{
	public interface IExpense : IPriceModifier
	{
		string Name { get; }
		Money ExpenseAmount { get; }
		string AsString(IFormatProvider res);
	}
}
