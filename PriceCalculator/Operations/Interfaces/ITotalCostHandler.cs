using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.Operations.Interfaces
{
	public interface ITotalCostHandler
	{
		ProductCosts GetResult(IProduct product, ProductCosts cost);
	}
}
