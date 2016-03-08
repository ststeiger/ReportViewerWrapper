
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Embedded2015
{


    public partial class _Default : System.Web.UI.Page
    {


        // Testing ReportViewer 2015
        protected void Page_Load(object sender, EventArgs e)
        {
            // Basic_SQL.SQL.GetSqlServerTypesDll(@"d:\");
            

            Microsoft.Reporting.WebForms.ReportViewer m_Viewer = new Microsoft.Reporting.WebForms.ReportViewer();

            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.WordOpenXml);
            // formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.Html);
            // formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.HtmlFragment);
            //formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.Image);
            formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.ExcelOpenXml);


            // m_Viewer.LocalReport.ReportPath = System.Web.Hosting.HostingEnvironment.MapPath("~/SimpleTest.rdl");

            using (System.IO.Stream reportDefinition = COR_Reports.ReportRepository.GetEmbeddedReport("SimpleTest.rdl"))
            {
                m_Viewer.LocalReport.LoadReportDefinition(reportDefinition);
            }
            

            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource();
            rds.Name = "DATA_Columns"; //This refers to the dataset name in the RDLC file
            string strSQL = @"SELECT TOP 100 * FROM information_schema.columns";
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
            if (!StringComparer.OrdinalIgnoreCase.Equals(System.Environment.UserDomainName, "COR"))
                savePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            
            savePath = System.IO.Path.Combine(savePath, "SimpleTest" + formatInfo.Extension);            
            System.IO.File.WriteAllBytes(savePath, result);
        } // End Sub Page_Load 


    } // End partial class _Default : System.Web.UI.Page


} // End Namespace Embedded2015
