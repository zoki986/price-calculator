using System;

namespace PriceCalculator.Models
{
	public class Money : IEquatable<Money>
	{
		public decimal Amount { get; }
		public IFormatProvider Format { get; }

		public Money(decimal amount, IFormatProvider format = default)
		{
			Amount = amount;
			Format = format;
		}
		public static Money operator +(Money a, Money b) => new Money(a.Amount + b.Amount);
		public static Money operator +(Money a, decimal b) => new Money(a.Amount + b);
		public static Money operator -(Money a, Money b) => new Money(a.Amount - b.Amount);
		public static Money operator -(Money a, decimal b) => new Money(a.Amount - b);
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

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public string AsString(IFormatProvider format)
		{
			return Amount.ToString("C", format);
		}

		public override string ToString()
		{
			return Amount.ToString("C", Format);
		}
	}
}
