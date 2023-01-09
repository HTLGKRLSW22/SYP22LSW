using System;
namespace UntisLib
{
#pragma warning disable IDE1006

    public class Lesson
    {
        public int id { get; set; }
        public int date { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public ShortData[] kl { get; set; } = null!;
        public ShortData[] te { get; set; } = null!;
        public ShortData[] su { get; set; } = null!;
        public ShortData[] ro { get; set; } = null!;
        public string? lstext { get; set; } = null!;
        public int lsnumber { get; set; }
        public string? activityType { get; set; } = "Unterricht";
        public string? code { get; set; }
        public string? info { get; set; }
        public string? substText { get; set; }
        public string? statflags { get; set; }
        public string? sg { get; set; }
        public string? bkRemark { get; set; }
        public string? bkText { get; set; }
    }

    public class ShortData
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string longname { get; set; } = null!;
    }
#pragma warning restore IDE1006

}
