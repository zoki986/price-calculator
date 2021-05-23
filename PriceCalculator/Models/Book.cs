using PriceCalculator.Interfaces;
using System;

namespace PriceCalculator.Models
{
	public class Book : IProduct
	{
		public string Name { get; }
		public int UPC { get; }
		public Money Price { get; set; }

		public Book(string name, int UPC, Money price)
		{
			Name = name;
			this.UPC = UPC;
			Price = price;
		}

		public IProduct WithNewPrice(Money price)
		{
			Price = price;
			return this;
		}

		public override string ToString()
		{
			return $"{Name}, UPC - {UPC} and Price - {Price}";
		}

		public string AsString(IFormatProvider formatProvider)
		{
			return $"{Name}, UPC - {UPC} and Price - {Price.Amount.ToString("C", formatProvider)}";
		}
	}
}
