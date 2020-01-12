using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Operations.Interfaces;

namespace PriceCalculator.Operations.Implementations
{
	public class PrecedenceHandler : IPrecedenceHandler
	{
		public IProduct GetResult(IProduct product, IPriceModifierBuilder priceModifiers)
		{
			return priceModifiers
				   .Discounts
				   .ApplyPrecedence(product, priceModifiers);
		}
	}
}
