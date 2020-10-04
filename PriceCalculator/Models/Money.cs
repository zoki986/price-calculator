namespace PriceCalculator.Models
{
	public class Money
	{
		public decimal Amount { get; }
		public string Simbol { get; }

		public Money(decimal amount, string simbol = "USD")
		{
			Amount = amount;
			Simbol = simbol;
		}
	}
}
