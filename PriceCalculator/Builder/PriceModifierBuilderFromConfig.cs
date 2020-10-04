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
					.WithPriceModifiers(priceConfig.PriceModifiers)
					.WithCurrencyFormat(priceConfig.CurrencyFormat)
					.WithCap(priceConfig.CapType);

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

			return JsonConvert.DeserializeObject<PriceConfig>(configContent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto});
		}

		private static Func<IEnumerable<IDiscount>, IProduct, decimal> GetDiscountCalculationMethod(PriceConfig priceConfig)
		{
			Func<IEnumerable<IDiscount>, IProduct, decimal> additive = PriceCalculationFunctions.SumDiscounts;
			Func<IEnumerable<IDiscount>, IProduct, decimal> multiply = PriceCalculationFunctions.MultypliDiscounts;
			return string.IsNullOrWhiteSpace(priceConfig.DiscountCalculationMode) || priceConfig.DiscountCalculationMode.Equals("additive") 
				? additive : multiply;
		}
		public static string GetApplicationRootPath()
		{
			var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
			Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
			var appRoot = appPathMatcher.Match(exePath).Value;
			return appRoot;
		}
	}

	public class CustomJsonConverter : JsonConverter<PriceConfig>
	{
		public override PriceConfig ReadJson(JsonReader reader, Type objectType, PriceConfig existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JObject jObject = JObject.Load(reader);
			//var tax = jObject["TaxPriceModifier"].ToObject<TaxPriceModifier>();
			//var discounts = jObject["Discounts"].ToList<JToken>();
			var discountList = new List<IPriceModifier>();

			//foreach (var disc in discounts)
			//{
			//	JProperty jProperty = disc.ToObject<JProperty>();
			//	discountList.Add((IDiscount)disc.ToObject(Type.GetType($"PriceCalculator.PriceModifiers.{jProperty.Name}")));
			//}

			PriceConfig pc = new PriceConfig();
			foreach (var prop in jObject)
			{
				var propType = Type.GetType($"PriceCalculator.PriceModifiers.{prop.Key}");

				if (propType != null)
					discountList.Add((IPriceModifier)JsonConvert.DeserializeObject(prop.Value.ToString(), propType));
				else
				{
					var key = prop.Key;
					var val = prop.Value;

					switch (key)
					{
						case
						"CurrencyFormat":
							pc.CurrencyFormat = val.Value<string>();
							break;
						case "DiscountCalculationMode":
							pc.DiscountCalculationMode = val.Value<string>();
							break;
						case "Cap":
							pc.Cap = val.Value<decimal>();
							break;
						default:
							break;
					}
				}
			}

			pc.PriceModifiers = discountList;

			return pc;
		}

		public override void WriteJson(JsonWriter writer,  PriceConfig value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
		private bool FieldExists(string fieldName, JObject jObject)
		{
			return jObject[fieldName] != null;
		}
	}
}
