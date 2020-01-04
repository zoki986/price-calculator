using StrategyDesignPattern.Models;

namespace StrategyDesignPattern.Interfaces
{
	public interface ITax
	{
		Money Cost { get; }
		int Precision { get; }
		IMoney ApllyPriceModifier(IProduct product);
	}
}
