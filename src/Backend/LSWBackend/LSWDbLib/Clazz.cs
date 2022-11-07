namespace LSWDbLib;

public class Clazz
{
    public int ClazzId { get; set; }
    public string ClazzName { get; set; } = null!;
    public int? TeacherId { get; set; }

    public Teacher? Teacher { get; set; }
    public virtual List<Student> Students { get; set; } = null!;
}
