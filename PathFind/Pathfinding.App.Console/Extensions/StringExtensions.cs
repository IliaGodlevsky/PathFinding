using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.Extensions
{
    internal static class StringExtensions
    {
        public static string ConvertCamelCaseToRegular(this string input)
        {
            var result = new StringBuilder();
            result.Append(input[0]);
            foreach (char c in input.Skip(1))
            {
                if (char.IsUpper(c))
                {
                    result.Append(' ');
                    result.Append(char.ToLower(c));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString().Trim();
        }
    }
}
