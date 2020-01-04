namespace StrategyDesignPattern.Interfaces
{
	public interface IExpense
	{
		string Name { get; }
		decimal Cost { get; }
		IMoney ApllyPriceModifier(IProduct product);
	}
}
