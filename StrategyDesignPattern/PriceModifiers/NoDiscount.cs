using StrategyDesignPattern.Interfaces;

namespace StrategyDesignPattern.PriceModifiers
{
	public class NoDiscount : IDiscount
	{
		public IMoney Amount { get; }

		public int Precision => 2;

		public bool HasPrecedence => false;

		public IMoney ApllyPriceModifier(IProduct product)
		{
			return product.Price;
		}
	}
}
