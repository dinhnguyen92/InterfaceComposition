using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace InterfaceComposition.Utils
{
    public static class StringExtensions
    {
        #region String Splits

        public static string[] TrimmedSplit(this string text, string[]? separators)
        {
              return text.Split(separators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        public static string[] SplitAsClauses(this string text)
        {
            return text.TrimmedSplit(new[] { ",", "and", "&" });
        }

        public static string[] SplitAsWords(this string text)
        {
            // When separators are null, the string will be split on whitespace characters
            return text.TrimmedSplit(null);
        }

        public static string[] SplitAsLines(this string text)
        {
            return text.TrimmedSplit(new[] { "\n" });
        }

        #endregion


        #region String Cases

        public static string ToTitleCase(this string text)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text).Trim();
        }

        public static string ToCamelCase(this string text)
        {
            return text.ToTitleCase().Replace(" ", string.Empty).Trim();
        }

        #endregion


        #region Punctuations

        public static string TrimTrailingPunctuations(this string text)
        {
            string result = text.Trim();

            // Recursively remove the last character as long as it's a punctuation
            if (!string.IsNullOrEmpty(result) && char.IsPunctuation(result.Last()))
            {
                result = result[0..^1];
                return result.TrimTrailingPunctuations();
            }
            return result;
        }

        #endregion


        #region Trimming

        public static string TrimPrefixIfMatchesIgnoreCase(this string text, string prefix)
        {
            if (text.StartsWithIgnoreCase(prefix))
            {
                return text[prefix.Length..].Trim();
            }
            return text;
        }

        public static string TrimLeadingPrefixIgnoreCase(this string text, string prefix)
        {
            string result = text.TrimPrefixIfMatchesIgnoreCase(prefix);

            // Recursively remove the prefix if it's still there
            if (!string.IsNullOrEmpty(result) && text.StartsWithIgnoreCase(prefix))
            {
                return result.TrimLeadingPrefixIgnoreCase(prefix);
            }
            return result;
        }

        public static string TrimSuffixIfMatchesIgnoreCase(this string text, string suffix)
        {
            if (text.EndsWithIgnoreCase(suffix))
            {
                return text[0..^suffix.Length].Trim();
            }
            return text;
        }

        public static string TrimTrailingSuffixIgnoreCase(this string text, string suffix)
        {
            string result = text.TrimSuffixIfMatchesIgnoreCase(suffix);

            // Recursively remove the suffix if it's still there
            if (!string.IsNullOrEmpty(result) && text.EndsWithIgnoreCase(suffix))
            {
                return result.TrimTrailingSuffixIgnoreCase(suffix);
            }
            return result;
        }

        #endregion


        #region String Comparisons

        public static bool EndsWithIgnoreCase(this string text, string suffix)
        {
            return text.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool EndsWithAnyIgnoreCase(this string text, IEnumerable<string> suffixes)
        {
            return suffixes.Any(text.EndsWithIgnoreCase);
        }

        public static bool StartsWithIgnoreCase(this string text, string prefix)
        {
            return text.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool StartsWithAnyIgnoreCase(this string text, IEnumerable<string> prefixes)
        {
            return prefixes.Any(text.StartsWithIgnoreCase);
        }

        public static bool EqualsIgnoreCase(this string text, string other)
        {
            return text.Equals(other, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion


        #region Plural/Singular

        // For now, we only pluralize text that contains a single word
        // TODO: maybe implement more sophisticated logic here (such as checking for words that are adjectives or already plural)
        public static bool CanPluralize(this string text)
        {
            return text.WordCount() == 1;
        }

        // This only works with common cases, e.g. "y" -> "ies", "x" -> "xes"
        // Also, this only works with texts containing a single word
        public static string ToPlural(this string text)
        {
            if (text.EndsWithAnyIgnoreCase(new[] { "s", "ss", "sh", "ch", "x", "z", "o" }))
            {
                return text + "es";
            }
            if (text.EndsWithIgnoreCase("y"))
            {
                // If the letter before the ending "y" is a vowel, then we simply add "s"
                if (text.Length >= 2 && text.ElementAt(text.Length - 2).IsVowel())
                {
                    return text + "s";
                }
                return text[0..^1] + "ies";
            }
            if (text.EndsWith("is"))
            {
                return text[0..^2] + "es";
            }
            if (text.EndsWith("on"))
            {
                return text[0..^2] + "a";
            }
            if (text.EndsWith("us"))
            {
                return text[0..^2] + "i";
            }
            return text + "s";
        }

        #endregion


        #region Miscellaneous

        public static int WordCount(this string text)
        {
            return text.SplitAsWords().Length;
        }

        #endregion
    }
}
