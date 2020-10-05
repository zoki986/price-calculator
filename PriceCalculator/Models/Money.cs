using System;

namespace PriceCalculator.Models
{
	public class Money : IEquatable<Money>
	{
		public decimal Amount { get; set; }
		public string Simbol { get; }

		public Money(decimal amount, string simbol = "USD")
		{
			Amount = amount;
			Simbol = simbol;
		}
		public Money()
		{
		}

		public static Money operator +(Money a, Money b) => new Money(a.Amount + b.Amount);
		public static Money operator +(Money a, decimal b) => new Money(a.Amount + b);
		public static Money operator -(Money a, Money b) => new Money(a.Amount - b.Amount);
		public static Money operator -(Money a, decimal b) => new Money(a.Amount - b);
		public static Money operator *(Money a, Money b) => new Money(a.Amount * b.Amount);
		public static Money operator *(Money a, decimal b) => new Money(a.Amount * b);

		public static bool operator ==(Money a, decimal b) => a.Amount.Equals(b);
		public static bool operator !=(Money a, decimal b) => !a.Amount.Equals(b);

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(Money other)
		{
			return other.Amount.Equals(Amount);
		}
	}
}
