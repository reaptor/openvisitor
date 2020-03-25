using System;

namespace OpenVisitor
{
    public static class ISO8601DateTimeExtensions
    {
        public static string ToISO8601String(this DateTime that) => that.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ");
        public static DateTime ISO8601StringToDate(this string that) => DateTime.Parse(that, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }
}