using System;
namespace UntisLib
{
#pragma warning disable IDE1006

    public class LessonInfo
    {
        public ResultLessons data { get; set; } = null!;
    }

    public class ResultLessons
    {
        public OverallData result { get; set; } = null!;
    }
#pragma warning restore IDE1006

}
