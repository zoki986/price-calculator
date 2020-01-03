namespace StrategyDesignPattern.Interfaces
{
	public interface IProduct
	{
		string Name { get; }
		int UPC { get; }
		IMoney Price { get; set; }
	}
}