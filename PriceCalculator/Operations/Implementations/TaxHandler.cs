using PriceCalculator.Interfaces;
using PriceCalculator.Operations.Interfaces;

namespace PriceCalculator.Operations.Implementations
{
	public class TaxHandler : ITaxHandler
	{
		public decimal GetResult(IProduct product, IPriceModifierBuilder priceModifiers)
		{
			return priceModifiers.Tax
				   .ApllyPriceModifier(product);
		}
	}
}
