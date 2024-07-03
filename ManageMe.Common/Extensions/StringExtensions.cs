using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ManageMe.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetSpaceNameFromCamelCase(this string input)
        {
            var result = string.Empty;

            foreach (var character in input)
            {
                if (char.IsUpper(character))
                {
                    result += " ";
                }

                result += character;
            }

            return result;
        }

        public static string GetAcronimFromSubjectName(this string input)
        {
            var result = string.Empty;

            foreach (var character in input)
            {
                if (char.IsUpper(character))
                {
                    result += character;
                }
            }

            return result;
        }
    }
}
