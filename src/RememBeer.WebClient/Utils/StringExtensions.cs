namespace RememBeer.WebClient.Utils
{
    public static class StringExtensions
    {
        public static string Crop(this string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Length < maxLength
                ? text
                : text.Substring(0, maxLength);
        }

        public static string Truncate(this string text, int maxLength, string end = "...")
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            if (text.Length < maxLength)
            {
                return text;
            }

            return text.Crop(maxLength) + end;
        }
    }
}
