using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Embedded2016
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Basic_SQL.SQL.GetSqlServerTypesDll(@"d:\Microsoft.SqlServer.Types.dll");
            // RenderReport();

            
            // byte[] baa = Portal_Reports.TestReport3.GetUmzugsmitteilung(new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.WordOpenXml), "C38CB749-1EEC-4686-9BBA-F627B9C4E8EC", "EN");
            // return;

            // RenderSimpleTestReport();
            // return;
            
            //byte[] ba = Portal_Reports.Umzugsmitteilung.GetUmzugsmitteilung(new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.WordOpenXml), "C38CB749-1EEC-4686-9BBA-F627B9C4E8EC", "DE");
            byte[] ba = Portal_Reports.Umzugsmitteilung.GetUmzugsmitteilung(new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.WordOpenXml), "C38CB749-1EEC-4686-9BBA-F627B9C4E8EC", "EN");
            return;

        }


        protected static void RenderSimpleTestReport()
        {
            Microsoft.Reporting.WebForms.ReportViewer m_Viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            // EnableFormat("foo");


            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.WordOpenXml);
            // formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.Html);
            // formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.HtmlFragment);
            //formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.Image);
            // formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.ExcelOpenXml);

            using (System.IO.Stream reportDefinition = COR_Reports.ReportRepository.GetEmbeddedReport("TestReport.rdl"))
            {
                m_Viewer.LocalReport.LoadReportDefinition(reportDefinition);
            }


            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource();
            rds.Name = "DataSet1"; //This refers to the dataset name in the RDLC file
            string strSQL = @"SELECT TOP 10 * FROM T_Benutzer";
            // strSQL = strSQL.Replace("@_in_umzugsuid", "'" + in_ump_uid.Replace("'", "''") + "'");
            // strSQL = strSQL.Replace("@_in_sprache", "'" + in_sprache.Replace("'", "''") + "'");
            rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
            strSQL = null;

            m_Viewer.LocalReport.DataSources.Add(rds);


            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] result = m_Viewer.LocalReport.Render(formatInfo.FormatName, formatInfo.DeviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            string savePath = @"D:\";
            if (!System.StringComparer.OrdinalIgnoreCase.Equals(System.Environment.UserDomainName, "COR"))
                savePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);

            savePath = System.IO.Path.Combine(savePath, "SimpleTest" + formatInfo.Extension);
            System.IO.File.WriteAllBytes(savePath, result);
        }


    }


}