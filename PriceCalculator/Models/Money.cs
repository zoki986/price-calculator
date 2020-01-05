using PriceCalculator.Interfaces;

namespace PriceCalculator.Models
{
	public class Money : IMoney
	{
		public decimal Amount { get; set; }
		public string Simbol { get; set; }
		public int Precision { get; }

		public Money()
		{
		}

		public Money(decimal ammount)
		{
			Amount = ammount;
		}

		public Money(IMoney money)
		{
			this.Amount = money.Amount;
		}

		public override string ToString()
		{
			return $"{Amount}{Simbol}";
		}
	}
}
