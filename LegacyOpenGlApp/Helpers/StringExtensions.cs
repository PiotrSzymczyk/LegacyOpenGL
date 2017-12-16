using System.Text.RegularExpressions;

namespace LegacyOpenGlApp.Helpers
{
	public static class StringExtensions
	{
		public static string SplitToLines(this string input, int maxLineLength = 100)
		{
			return Regex.Replace(input + " ", $@"(.{{1,{maxLineLength - 1}}}[ \.\,\$])", "$1\n");
		}
	}
}
