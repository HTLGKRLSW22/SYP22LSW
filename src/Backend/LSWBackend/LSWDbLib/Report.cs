namespace LSWDbLib;

public class Report
{
    public int ReportId { get; set; }
    public string ReportDocumentEncoded { get; set; } = null!;
    public int OfferId { get; set; }

    public Offer Offer { get; set; } = null!;
    public virtual List<ReportImage> ReportImages { get; set; } = null!;
}
