using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator.PriceCalculationStrategies
{
	class AbstractOperation
	{
		IList<IOperation> operations;
	}

	internal interface IOperation
	{
		void Execute();
	}
}
