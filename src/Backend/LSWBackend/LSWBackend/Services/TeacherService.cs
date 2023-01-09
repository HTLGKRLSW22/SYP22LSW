using System;

using UntisLib;
namespace LSWBackend.Services;

public class TeacherService
{
    public Dictionary<string, int> AmountHoursFromTeachers(int dateAsInt) => UntisLib.UntisProgram.AmountHoursFromTeachers(dateAsInt);
}

