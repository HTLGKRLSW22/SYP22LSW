using System;
using System.Collections.Generic;

namespace UntisLib
{
#pragma warning disable IDE1006
    public class OverallData
    {
        public Data data { get; set; } = null!;
        public long lastImportTimestamp { get; set; }
    }

    public class Data
    {
        public bool? noDetails { get; set; }
        public int[]? elementIds { get; set; }
        public Dictionary<string, PeriodElement[]> elementPeriods { get; set; } = null!;
        public Element[] elements { get; set; } = null!;
    }

    public class PeriodElement
    {
        public int? date { get; set; }
        public int type { get; set; }
        public Element[] elements { get; set; } = null!;

    }

    public class Element
    {
        public int type { get; set; }
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string longName { get; set; } = null!;
        public string displayname { get; set; } = null!;
        public string alternatename { get; set; } = null!;
        public bool canViewTimetable { get; set; }
        public int roomCapacity { get; set; }
    }
#pragma warning restore IDE1006

}

