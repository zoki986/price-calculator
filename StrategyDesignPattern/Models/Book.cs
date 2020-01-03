using StrategyDesignPattern.Interfaces;
using System;

namespace StrategyDesignPattern.Models
{
	public class Book : IProduct
	{
		public string Name { get; }
		public int UPC { get; }
		public IMoney Price { get; set; }

		public Book(string name, int UPC, IMoney price)
		{
			Name = name;
			this.UPC = UPC;
			Price = price;
		}

		public override string ToString()
		{
			return $"Name - {Name}, UPC - {UPC} and Price - {Price}";
		}
	}
}
