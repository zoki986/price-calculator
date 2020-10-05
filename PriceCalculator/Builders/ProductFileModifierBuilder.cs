using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
	public static class ProductFileModifierBuilder
	{
		public static ProductModifiersBuilder GetPriceModifierBuilder(string filePath)
		{
			ProductModifiersBuilder modifiersBuilder = new ProductModifiersBuilder();
			try
			{
				if (string.IsNullOrWhiteSpace(filePath))
					throw new ArgumentNullException(nameof(filePath));

				FileModifiersConfig priceConfig = ReadConfigFile(filePath);

				modifiersBuilder
					.WithPriceModifiers(priceConfig.PriceModifiers)
					.WithCurrencyFormat(priceConfig.CurrencyFormat)
					.WithCap(priceConfig.CapType);

			}
			catch (Exception e)
			{
				Console.WriteLine("Error reading from file");
				Console.WriteLine(e.Message);
			}

			return modifiersBuilder;
		}

		private static FileModifiersConfig ReadConfigFile(string filePath)
		{
			var root = GetApplicationRootPath();
			var configContent = File.ReadAllText(Path.Combine(root, filePath));

			return JsonConvert.DeserializeObject<FileModifiersConfig>(
				configContent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects});
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
