namespace Framework.Business.Extension
{
    using System;
    using System.Globalization;
    using TimeZoneConverter;

    /// <summary>
    /// Date and time extension methods.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Parses the given string as a <see cref="DateTime"/>, respecting the given time zone that it is intended to be in.
        /// </summary>
        /// <param name="sourceDateTime">The string to parse as a date/time.</param>
        /// <param name="sourceTimeZoneId">The time zone the source date/time is in.</param>
        /// <returns>The parsed <see cref="DateTimeOffset"/> with the source's time zone represented.</returns>
        public static DateTimeOffset ParseDateTimeWithTimeZone(string sourceDateTime, string sourceTimeZoneId)
        {
            var parsedDateTime = DateTime.Parse(sourceDateTime, null);
            var sourceTimeZoneInfo = TZConvert.GetTimeZoneInfo(sourceTimeZoneId);
            return TimeZoneInfo.ConvertTimeToUtc(parsedDateTime, sourceTimeZoneInfo);
        }

        /// <summary>
        /// Parses the given string as a <see cref="DateTime"/>, respecting the given time zone that it is intended to be in.
        /// </summary>
        /// <param name="sourceDateTime">The string to parse as a date/time.</param>
        /// <param name="sourceTimeZoneId">The time zone the source date/time is in.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <param name="parsedDateTime">The parsed date time.</param>
        /// <returns>The parsed <see cref="DateTimeOffset"/> with the source's time zone represented.</returns>
        public static bool TryParseExactWithTimeZone(string sourceDateTime, string sourceTimeZoneId, string format, IFormatProvider provider, DateTimeStyles style, out DateTimeOffset parsedDateTime)
        {
            var parsed = DateTime.TryParseExact(sourceDateTime, format, provider, style, out var datetime);

            if (parsed)
            {
                var sourceTimeZoneInfo = TZConvert.GetTimeZoneInfo(sourceTimeZoneId);
                parsedDateTime = TimeZoneInfo.ConvertTimeToUtc(datetime, sourceTimeZoneInfo);
            }
            else
            {
                parsedDateTime = DateTime.MinValue;
            }

            return parsed;
        }

        /// <summary>
        /// Parses the given datetime, respecting the given time zone that it is intended to be in.
        /// </summary>
        /// <param name="currentTimeZone">The current time zone the source date/time is in.</param>
        /// <param name="dateTime">The string to parse as a date/time.</param>
        /// <returns>The parsed <see cref="DateTimeOffset"/> with the source's time zone represented.</returns>
        public static DateTimeOffset FormatDateTimeToCurrentTimeZone(string currentTimeZone, DateTimeOffset dateTime)
        {
            var timeZoneInfo = TZConvert.GetTimeZoneInfo(currentTimeZone);

            return TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);
        }
    }
}
