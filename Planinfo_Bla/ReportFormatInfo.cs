
namespace COR_Reports
{

    // EXCEL / PDF / WORD (not supported in 2005)
    public enum ExportFormat
    {
        PDF
       ,Excel
       ,Html
       ,HtmlFragment
       ,Unknown = 666
    } // End enum ExportFormat


    public class ReportFormatInfo
    {
        public string Extension;
        public ExportFormat Format;
        public string FormatName;
        public string Mime;
        public string DeviceInfo;

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
                case ExportFormat.HtmlFragment:
                    this.Extension = ".htm";
                    this.FormatName = "HTML4.0";
                    this.Mime = "text/html";
                    this.Format = pFormat;
                    // https://msdn.microsoft.com/en-us/library/ms155395.aspx
                    // #oReportCell { width: 100%; }
                    // JavaScript:   Indicates whether JavaScript is supported in the rendered report. 
                    //               The default value is true.
                    // HTMLFragment: Indicates whether an HTML fragment is created in place of a full HTML document. 
                    //               An HTML fragment includes the report content in a TABLE element and omits the HTML and BODY elements. 
                    //               The default value is false.
                    // StyleStream:  Indicates whether styles and scripts are created as a separate stream instead of in the document. 
                    //               The default value is false.
                    // StreamRoot:   The path used for prefixing the value of the src attribute of the IMG element in the HTML report returned by the report server. 
                    //               By default, the report server provides the path. 
                    //               You can use this setting to specify a root path for the images in a report (for example, http://<servername>/resources/companyimages).
                    // <StreamRoot>/ReportServer/Resources</StreamRoot>
                    this.DeviceInfo = @"<DeviceInfo><HTMLFragment>True</HTMLFragment><JavaScript>false</JavaScript><StyleStream>true</StyleStream></DeviceInfo>";
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
