using System;
namespace NukedBit.Core
{
    public static class StringExtensions
    {
        public static bool IsEmptyOrNull(this string s) => string.IsNullOrEmpty(s);
        public static bool IsEmptyOrNullOrWhiteSpace(this string s) => string.IsNullOrWhiteSpace(s);
    }
}
