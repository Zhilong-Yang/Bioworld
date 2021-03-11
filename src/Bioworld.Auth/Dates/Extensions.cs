namespace Bioworld.Auth.Dates
{
    using System;

    internal static class Extensions
    {
        public static long ToTimestamp(this DateTime dateTime) =>
            new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    }
}