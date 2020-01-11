using PriceCalculator.Interfaces;
using System;
using System.Globalization;

namespace PriceCalculator.Models
{
	public class Book : IProduct
	{
		public string Name { get; }
		public int UPC { get; }
		public decimal Price { get; set; }
		public Book()
		{
		}

		public Book(string name, int UPC, decimal price)
		{
			Name = name;
			this.UPC = UPC;
			Price = price;
		}

		public override string ToString()
		{
			return $"{Name}, UPC - {UPC} and Price - {Price}";
		}

		public string AsString(NumberFormatInfo formatInfo)
		{
			return $"{Name}, UPC - {UPC} and Price - {Price.ToString("C", formatInfo)}";
		}
	}
}
