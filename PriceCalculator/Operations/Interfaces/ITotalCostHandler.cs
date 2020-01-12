using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.Operations.Interfaces
{
	public interface ITotalCostHandler
	{
		Costs GetResult(IProduct product, Costs cost);
	}
}
