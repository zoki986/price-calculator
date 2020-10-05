using PriceCalculator.Builder;
using PriceCalculator.Models;

namespace PriceCalculator.Interfaces
{
	public interface IPriceCalculation
	{
		PriceCalculationResult GetPriceResultForProduct(IProduct product, IProductModifiersBuilder priceModifiers);
	}
}
