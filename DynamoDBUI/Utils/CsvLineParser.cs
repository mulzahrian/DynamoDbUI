using System.Collections.Generic;
using System.Text;

namespace DynamoDBUI.Utils
{
    /// <summary>
    /// Parser sederhana untuk memecah list dipisah koma,
    /// dengan dukungan tanda kutip (single/double) supaya
    /// value seperti 'Jakarta, Indonesia' tidak terpecah.
    /// </summary>
    public static class CsvLineParser
    {
        public static List<string> Split(string input)
        {
            var result = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;
            char quoteChar = '\0';

            foreach (char c in input)
            {
                if (inQuotes)
                {
                    if (c == quoteChar)
                        inQuotes = false;
                    current.Append(c);
                }
                else
                {
                    if (c == '\'' || c == '"')
                    {
                        inQuotes = true;
                        quoteChar = c;
                        current.Append(c);
                    }
                    else if (c == ',')
                    {
                        result.Add(current.ToString().Trim());
                        current.Clear();
                    }
                    else
                    {
                        current.Append(c);
                    }
                }
            }

            if (current.Length > 0)
                result.Add(current.ToString().Trim());

            return result;
        }

        public static string TrimQuotes(string value)
        {
            if (value.Length >= 2)
            {
                if ((value.StartsWith("'") && value.EndsWith("'")) ||
                    (value.StartsWith("\"") && value.EndsWith("\"")))
                {
                    return value.Substring(1, value.Length - 2);
                }
            }
            return value;
        }
    }
}