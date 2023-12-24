using TimeZoneConverter;

namespace DTOs.Extended;
public static class LocalTime
{
    public static DateTime GetUtc5Time()
    {
        DateTime utcTime = DateTime.UtcNow;
        TimeZoneInfo timeZone = TZConvert.GetTimeZoneInfo("Pakistan Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
    }
}