﻿using System.Globalization;

namespace StrategyDesignPattern.Interfaces
{
	public interface IProduct
	{
		string Name { get; }
		int UPC { get; }
		IMoney Price { get; set; }
		string AsString(NumberFormatInfo formatInfo);
	}
}