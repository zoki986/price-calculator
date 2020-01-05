using Newtonsoft.Json;
using PriceCalculator.Interfaces;
using PriceCalculator.Models;
using PriceCalculator.PriceCalculationStrategies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PriceCalculator.Builder
{
	public static class PriceModifierBuilderFromConfig
	{
		public static PriceModifiersBuilder GetPriceModifierBuilder(string filePath)
		{
			PriceModifiersBuilder modifiersBuilder = new PriceModifiersBuilder();
			try
			{
				if (string.IsNullOrWhiteSpace(filePath))
					throw new ArgumentNullException(nameof(filePath));

				PriceConfig priceConfig = ReadConfigFile(filePath);

				modifiersBuilder
					.WithTax(priceConfig.Tax)
					.WithDiscount(priceConfig.Discounts)
					.WithExpense(priceConfig.AdditionalExpenses)
					.WithCurrencyFormat(priceConfig.CurrencyFormat)
					.WithCap(priceConfig.Cap, priceConfig.CapType);

				modifiersBuilder.DiscountCalculationMode = GetDiscountCalculationMethod(priceConfig);

			}
			catch (Exception e)
			{
				Console.WriteLine("Error reading from file");
				Console.WriteLine(e.Message);
			}

			return modifiersBuilder;
		}

		private static PriceConfig ReadConfigFile(string filePath)
		{
			var root = GetApplicationRootPath();
			var configContent = File.ReadAllText(Path.Combine(root, filePath));

			return JsonConvert.DeserializeObject<PriceConfig>(configContent);
		}

		private static Func<IEnumerable<IDiscount>, IProduct, IMoney> GetDiscountCalculationMethod(PriceConfig priceConfig)
		{
			Func<IEnumerable<IDiscount>, IProduct, IMoney> additive = PriceCalculationFunctions.SumDiscounts;
			Func<IEnumerable<IDiscount>, IProduct, IMoney> multiply = PriceCalculationFunctions.MultypliDiscounts;
			return string.IsNullOrWhiteSpace(priceConfig.DiscountCalculationMode) || priceConfig.DiscountCalculationMode.Equals("additive") ? additive : multiply;
		}
		public static string GetApplicationRootPath()
		{
			var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
			Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
			var appRoot = appPathMatcher.Match(exePath).Value;
			return appRoot;
		}
	}
}
