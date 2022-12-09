using System.Globalization;

namespace Domain.Models;

public class DateTime
{
    public long Epoch { get; set; }
    private int Year { get; set; }
    private int Month { get; set; }
    private int Day { get; set; }
    private int Hour { get; set; }
    private int Minute { get; set; }
    private int Second { get; set; }

    public DateTime(int month, int day, int year, int hour, int minute, int second)
    {
        this.Year = year;
        this.Month = month;
        this.Day = day;
        this.Hour = hour;
        this.Minute = minute;
        this.Second = second;
        
        var dateTime = new System.DateTime(Year, Month, Day, Hour, Minute, Second, DateTimeKind.Utc);
        var dateWithOffset = new DateTimeOffset(dateTime).ToUniversalTime();
        Epoch = dateWithOffset.ToUnixTimeSeconds();
    }

    public DateTime(int month, int day, int year)
    {
        this.Year = year;
        this.Month = month;
        this.Day = day;
    }
    
    public DateTime()
    {
        
    }

    public DateTime(long epoch)
    {
        this.Epoch = epoch;
        this.Year = 0;
        this.Month = 0;
        this.Day = 0;
        this.Hour = 0;
        this.Minute = 0;
        this.Second = 0;
    }

    public string GetTimeString()
    {
        epoch2string(Epoch);

        string s = "";

        if (Hour < 10)
        {
            s += "0";
        }

        s += Hour;
        s += ":";
        if (Minute < 10)
        {
            s += "0";
        }

        s += Minute;

        return s + " ";

    }

    public string GetDateString()
    {
        epoch2string(Epoch);
        return Month + "/" + Day + "/" + Year;
    }

    public string FullDateAndTimeString()
    {
        return GetTimeString() + GetDateString();
    }

    public string GetFormattedString()
    {
        double seconds = Epoch - DateTimeOffset.Now.ToUnixTimeSeconds();

        double interval = seconds / 31536000;

        if (interval > 1) {
            return "in " + Math.Round(interval) + " years";
        }
        interval = seconds / 2592000;
        if (interval > 1) {
            return "in " + Math.Round(interval) + " months";
        }
        interval = seconds / 86400;
        if (interval > 1) {
            return "in " + Math.Round(interval) +  " days";
        }
        interval = seconds / 3600;
        if (interval > 1) {
            return "in " + Math.Round(interval) + " hours";
        }
        interval = seconds / 60;
        if (interval > 1) {
            return "in " + Math.Round(interval) + " minutes";
        }
        return "in " + Math.Round(seconds) + " seconds";         
        
    }

    
    private void epoch2string(long epoch) {
        System.DateTime date = new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch);
        Year = date.Year;
        Month = date.Month;
        Day = date.Day;
        Hour = date.Hour;
        Minute = date.Minute;
        Second = date.Second;
    }
}