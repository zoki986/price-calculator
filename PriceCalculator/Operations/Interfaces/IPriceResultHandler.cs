using PriceCalculator.Interfaces;
using PriceCalculator.Models;

namespace PriceCalculator.Operations.Interfaces
{
	public interface IPriceResultHandler
	{
		PriceCalculationResult GetResult(IProduct product, IPriceModifierBuilder priceModifiers, ProductCosts costs);
	}
}
