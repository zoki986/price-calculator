using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StrategyDesignPattern.Formaters
{
	public class BaseCurrencyFormater
	{
		static NumberFormatInfo nf = new NumberFormatInfo();

		public BaseCurrencyFormater()
		{
		}

		public static string FormatCurrencyWithSimbol(decimal amount,string simbol)
		{
			if (string.IsNullOrWhiteSpace(simbol) && Regex.IsMatch(simbol, "[A-Z]{3}"))
				simbol = "USD";
	
			nf.CurrencySymbol = simbol;
			nf.CurrencyPositivePattern = 3;
			return amount.ToString("c", nf);
		}

		public static string FormatCurrency(decimal amount, string currencyCode)
		{
			var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
						   let r = new RegionInfo(c.LCID)
						   where r != null
						   && r.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
						   select c).FirstOrDefault();

			if (culture == null)
				return amount.ToString("0.00");

			return string.Format(culture, "{0:C}", amount);
		}
	}
}
