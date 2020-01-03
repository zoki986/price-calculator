namespace StrategyDesignPattern.Interfaces
{
	public interface IDiscount
	{
		IMoney Amount { get;}
		int Precision { get; }
		bool HasPrecedence { get; }
		IMoney ApllyPriceModifier(IProduct product);
	}
}