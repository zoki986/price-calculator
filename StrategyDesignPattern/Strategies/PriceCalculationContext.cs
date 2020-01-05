﻿using StrategyDesignPattern.Builder;
using StrategyDesignPattern.Interfaces;
using StrategyDesignPattern.PriceCalculationStrategies;
using StrategyDesignPattern.Reporters;
using System;

namespace StrategyDesignPattern.Strategies
{
	public class PriceCalculationContext
	{
		private IPriceCalculation _calculator = new PriceCalculator();
		private PriceModifiersBuilder _priceModifiers;
		public PriceCalculationContext() { }

		public PriceCalculationContext(PriceModifiersBuilder priceModifiers)
		{
			_priceModifiers = priceModifiers ?? throw new ArgumentNullException(nameof(priceModifiers));
		}

		public void SetModifiers(PriceModifiersBuilder priceModifiers)
		{
			_priceModifiers = priceModifiers ?? throw new ArgumentNullException(nameof(priceModifiers));
		}

		public void CalculateAndReportPrice(IProduct product)
		{
			ConsoleReporter.PrintResult(_calculator.GetPriceResultForProduct(product, _priceModifiers));
		}
	}
}
