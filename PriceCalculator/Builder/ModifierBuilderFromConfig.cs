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
	public static class ModifierBuilderFromConfig
	{
		public static ModifiersBuilder GetPriceModifierBuilder(string filePath)
		{
			ModifiersBuilder modifiersBuilder = new ModifiersBuilder();
			try
			{
				if (string.IsNullOrWhiteSpace(filePath))
					throw new ArgumentNullException(nameof(filePath));

				FilePriceConfig priceConfig = ReadConfigFile(filePath);

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

		private static FilePriceConfig ReadConfigFile(string filePath)
		{
			var root = GetApplicationRootPath();
			var configContent = File.ReadAllText(Path.Combine(root, filePath));

			return JsonConvert.DeserializeObject<FilePriceConfig>(configContent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto});
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
