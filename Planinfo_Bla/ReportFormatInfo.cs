
namespace COR_Reports
{

    // EXCEL / PDF / WORD (not supported in 2005)
    public enum ExportFormat
    {
        PDF
       ,Excel
           ,Html
       ,Unknown = 666
    } // End enum ExportFormat


    public class ReportFormatInfo
    {
        public string Extension;
        public ExportFormat Format;
        public string FormatName;
        public string Mime;


        public ReportFormatInfo()
            : this(ExportFormat.PDF)
        { }


        public ReportFormatInfo(ExportFormat pFormat)
        {
            switch (pFormat)
            {
                case ExportFormat.Excel:
                    this.Extension = ".xls";
                    this.FormatName = "EXCEL";
                    this.Mime = @"application/vnd.ms-excel";
                    this.Format = pFormat;
                    break;
                case ExportFormat.PDF:
                    this.Extension = ".pdf";
                    this.FormatName = "PDF";
                    this.Mime = "application/pdf";
                    this.Format = pFormat;
                    break;
                case ExportFormat.Html:
                    this.Extension = ".htm";
                    this.FormatName = "HTML4.0";
                    this.Mime = "text/html";
                    this.Format = pFormat;
                    break;
                default:
                    this.Extension = ".pdf";
                    this.FormatName = "PDF";
                    this.Mime = "application/pdf";
                    this.Format = ExportFormat.PDF;
                    break;
            } // End Switch 

        } // End Constructor 


    } // End Class ReportFormatInfo


} // End Namespace COR_Reports 
