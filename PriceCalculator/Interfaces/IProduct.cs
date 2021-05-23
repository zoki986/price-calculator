using PriceCalculator.Models;
using System;

namespace PriceCalculator.Interfaces
{
	public interface IProduct
	{
		string Name { get; }
		int UPC { get; }
		Money Price { get; set; }
		IProduct WithNewPrice(Money price);
		string AsString(IFormatProvider formatInfo);
	}
}