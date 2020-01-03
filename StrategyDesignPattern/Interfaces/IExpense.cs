namespace StrategyDesignPattern.Interfaces
{
	public interface IExpense
	{
		string Name { get; }
		decimal Cost { get; }
		int Precision { get; }
		IMoney ApllyPriceModifier(IProduct product);
	}
}
