namespace LSWDbLib;

public class ReportImage
{
    public int ReportImageId { get; set; }
    public string ReportImageEncoded { get; set; } = null!;
    public int ReportId { get; set; }

    public Report Report { get; set; } = null!;
}
