using StrategyDesignPattern.Models;

namespace StrategyDesignPattern.Interfaces
{
	public interface IDiscount
	{
		Money DiscountAmount { get;}
		int Precision { get; }
		bool HasPrecedence { get; }
		IMoney ApllyPriceModifier(IProduct product);
	}
}