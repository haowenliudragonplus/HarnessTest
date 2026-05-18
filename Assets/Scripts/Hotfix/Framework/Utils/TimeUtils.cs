using System;
using DragonPlus.Core;
using DragonPlus.Network;
using UnityEngine;

public static class TimeUtils
{
    public const int SecPerMinute = 60; //一分钟的秒数
    public const int SecPerHour = 3600; //一小时的秒数
    public const int SecPerDay = 86400; //一天的秒数

    private static DateTime StartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// 获取当前服务器时间戳（秒）
    /// </summary>
    public static long GetServerTimeStamp()
    {
        var timeStamp = SDK<INetwork>.Instance.GetValidateServerTime() / 1000;
        return timeStamp;
    }

    /// <summary>
    /// 获取当前本地时间戳（毫秒）
    /// </summary>
    public static long GetLocalTimeStamp()
    {
        long timeStamp_ms = (long)(DateTime.UtcNow - StartTime).TotalMilliseconds;
        return timeStamp_ms;
    }

    /// <summary>
    /// 格式化时间
    /// </summary>
    /// useColon：是否使用冒号
    public static string FormatTime(long sec, bool useColon = true)
    {
        string timeStr = "";
        TimeSpan timeSpan = new TimeSpan(0, 0, 0, (int)sec);
        if (timeSpan.Days > 0)
        {
            timeStr = CoreUtils.GetLocalization(useColon ? "UI_TimeFormatWithDay_2" : "UI_TimeFormatWithDay", timeSpan.Days, timeSpan.Hours);
        }
        else if (timeSpan.Hours > 0)
        {
            timeStr = CoreUtils.GetLocalization(useColon ? "UI_TimeFormatWithHour_2" : "UI_TimeFormatWithHour", timeSpan.Hours, timeSpan.Minutes);
        }
        else
        {
            timeStr = CoreUtils.GetLocalization(useColon ? "UI_TimeFormatWithMinute_2" : "UI_TimeFormatWithMinute", timeSpan.Minutes, timeSpan.Seconds);
        }
        return timeStr;
    }

    /// <summary>
    /// 格式化日期
    /// </summary>
    public static string FormatDateTime(DateTime dt, string format = "yyyy/MM/dd HH:mm:ss")
    {
        return dt.ToString(format);
    }

    /// <summary>
    /// 格式化日期
    /// </summary>
    public static string FormatDateTime(double timeStamp, string format = "yyyy/MM/dd HH:mm:ss")
    {
        var dateTime = StartTime.AddSeconds(timeStamp);
        string ret = FormatDateTime(dateTime, format);
        return ret;
    }

    /// <summary>
    /// 获取间隔的秒数
    /// </summary>
    public static long GetSecondsInterval(DateTime dt1, DateTime dt2)
    {
        TimeSpan ts = dt2 - dt1;
        var sec = (long)ts.TotalSeconds;
        return sec;
    }

    /// <summary>
    /// 获取间隔的小时
    /// </summary>
    public static int GetHourInterval(long seconds)
    {
        int hour = (int)Mathf.Floor(seconds / SecPerHour);
        return hour;
    }

    /// <summary>
    /// 获取间隔的小时
    /// </summary>
    public static int GetHourInterval(long timeStamp1, long timeStamp2)
    {
        var sec = timeStamp2 - timeStamp1;
        int hour = GetHourInterval(sec);
        return hour;
    }

    /// <summary>
    /// 获取间隔的天数
    /// </summary>
    public static int GetDayInterval(long seconds)
    {
        int day = (int)Mathf.Floor(seconds / SecPerDay);
        return day;
    }

    /// <summary>
    /// 获取间隔的天数
    /// </summary>
    public static int GetDayInterval(long timeStamp1, long timeStamp2)
    {
        var sec = timeStamp2 - timeStamp1;
        int day = GetDayInterval(sec);
        return day;
    }

    /// <summary>
    /// 判断两个时间戳间隔几天（基于自然日计算）
    /// </summary>
    public static int GetDayIntervalByTimeStamp(long timeStamp1, long timeStamp2)
    {
        var dt1 = GetDateTime(timeStamp1);
        var dt2 = GetDateTime(timeStamp2);
        
        // 只计算日期部分的间隔，忽略时间部分
        DateTime date1 = dt1.Date;
        DateTime date2 = dt2.Date;
        
        // 计算日期间隔，可以返回负数
        TimeSpan ts = date2 - date1;
        int day = (int)ts.TotalDays;
        
        return day;
    }
    
    /// <summary>
    /// 获取是当前周的第几天
    /// </summary>
    private static int GetDayOfWeek(DayOfWeek dayOfWeekType)
    {
        if (dayOfWeekType == DayOfWeek.Sunday)
        {
            return 7;
        }
        return (int)dayOfWeekType;
    }

    /// <summary>
    /// 获取时间戳（秒）
    /// </summary>
    public static long GetTimeStamp(DateTime dateTime)
    {
        var sec = (long)(dateTime - StartTime).TotalSeconds;
        return sec;
    }

    /// <summary>
    /// 根据时间戳转换为DateTime
    /// </summary>
    public static DateTime GetDateTime(long timeStamp)
    {
        var dt = StartTime.AddSeconds(timeStamp);
        return dt;
    }

    /// <summary>
    /// 是否为同一天（传入的参数单位是秒）
    /// </summary>
    public static bool IsSameDay(long timeStamp1, long timeStamp2)
    {
        var dt1 = GetDateTime(timeStamp1);
        var dt2 = GetDateTime(timeStamp2);
        var isSameDay = dt1.Day == dt2.Day;
        return isSameDay;
    }
}