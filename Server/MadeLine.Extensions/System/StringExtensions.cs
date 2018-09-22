namespace System
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string SeparateCamelCase(this string s)
        {
            if (s == null) return null;
            var sb = new StringBuilder(s.Length);
            sb.Append(s[0]);
            for (int i = 1; i < s.Length; i++)
            {
                if (char.IsUpper(s[i]))
                {
                    sb.Append(' ');
                }

                sb.Append(s[i]);
            }

            return sb.ToString();
        }

        public static IList<int> FindAllIndexes(this string text, string pattern)
        {
            var indexes = new List<int>();
            foreach (Match match in Regex.Matches(text, pattern))
            {
                indexes.Add(match.Index);
            }

            return indexes;
        }

        public static string FormatWith(this string s, params object[] args)
        {
            return string.Format(s, args);
        }

        public static string SubstringFrom(this string s, string separator)
        {
            var index = s.IndexOf(separator) + 1;
            return s.Substring(index);
        }

        public static string SubstringFromLast(this string s, string separator)
        {
            var index = s.LastIndexOf(separator) + 1;
            return s.Substring(index);
        }
    }
}
