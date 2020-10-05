using PriceCalculator.Models;
using System.Globalization;

namespace PriceCalculator.Interfaces
{
	public interface IProduct
	{
		string Name { get; }
		int UPC { get; }
		Money Price { get; set; }
		IProduct WithPrice(Money price);
		string AsString(NumberFormatInfo formatInfo);
	}
}