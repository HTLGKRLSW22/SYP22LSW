using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib;

public class Report
{
    public int ReportId { get; set; }
    public string ReportDocumentEncoded { get; set; }
    public int OfferId { get; set; }

    public Offer Offer { get; set; }
    public virtual List<ReportImage> ReportImages { get; set; }
}
