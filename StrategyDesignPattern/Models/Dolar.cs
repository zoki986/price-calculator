using StrategyDesignPattern.Common;
using StrategyDesignPattern.Formaters;
using StrategyDesignPattern.Interfaces;

namespace StrategyDesignPattern.Models
{
	public class Dolar : IMoney
	{
		public decimal Ammount { get; set; }
		public string Simbol { get; set; }
		public int Precision { get; }

		public Dolar(decimal ammount)
		{
			Ammount = ammount;
		}

		public Dolar(IMoney money)
		{
			this.Ammount = money.Ammount;
		}

		public override string ToString()
		{
			return $"{Ammount}{Simbol}";
		}
	}
}
