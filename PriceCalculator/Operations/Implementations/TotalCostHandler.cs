using PriceCalculator.Common;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.Operations.Interfaces;

namespace PriceCalculator.Operations.Implementations
{
	public class TotalCostHandler : ITotalCostHandler
	{
		public ProductCosts GetResult(IProduct product, ProductCosts costs)
		{
			var total = product
					   .Price
					   .SumWith(costs.Tax)
					   .SumWith(costs.Expenses)
					   .Substract(costs.Discounts)
				       .WithPrecision(Constants.DefaultPrecision);

			return new ProductCosts(costs, total);
		}
	}
}
