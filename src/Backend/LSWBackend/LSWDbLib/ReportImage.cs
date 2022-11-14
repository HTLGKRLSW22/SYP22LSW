using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib;

public class ReportImage
{
    public int ReportImageId { get; set; }
    public string ReportImageEncoded { get; set; }
    public int ReportId { get; set; }

    public Report Report { get; set; }
}
