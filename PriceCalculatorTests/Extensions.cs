namespace PriceCalculatorTests
{
	public static class Extensions
	{
		public static string CleanseString(this string src)
		{
			return src.Replace("\r\n", " ").Trim();
		}
	}
}
