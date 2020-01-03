namespace StrategyDesignPattern.Interfaces
{
	public interface ITax
	{
		IMoney Cost { get; }
		int Precision { get; }
		IMoney ApllyPriceModifier(IProduct product);
	}
}
