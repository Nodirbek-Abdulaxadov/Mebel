namespace DTOs;
public static class DateTimeParse
{
    public static DateTime ToDateTime(this string date)
    {
        int[] dates = date.Split('-').Select(int.Parse).ToArray();
        return new DateTime(dates[0], dates[1], dates[2]);
    }
}