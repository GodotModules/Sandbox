using System.Globalization;

namespace GodotModules 
{
    public static class StringExtensions 
    {
        public static string ToTitleCase(this string v) =>
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(v.ToLower());

        public static string SmallWordsToUpper(this string v, int maxLength = 2, Func<string, bool> filter = null)
        {
            var words = v.Split(' ');

            for (var i = 0; i < words.Length; i++)
                if (words[i].Length <= maxLength && (filter == null || filter(words[i])))
                    words[i] = words[i].ToUpper();

            return string.Join(" ", words);
        }
    }
}