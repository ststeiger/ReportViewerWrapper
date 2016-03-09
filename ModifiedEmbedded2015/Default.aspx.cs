
namespace ModifiedEmbedded2015
{


    public partial class _Default : System.Web.UI.Page
    {


        // http://web.archive.org/web/20120101201615/http://beaucrawford.net/post/Enable-HTML-in-ReportViewer-LocalReport.aspx
        private void EnableFormat(string formatName)
        {
            const System.Reflection.BindingFlags Flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;

            System.Type tt = this.m_Viewer.LocalReport.GetType();
            System.Reflection.FieldInfo m_previewService = tt.GetField("m_previewService", Flags);

            if (m_previewService == null)
                m_previewService = tt.GetField("m_processingHost", Flags);

            // Works only for v2005
            if (m_previewService != null)
            {

                System.Reflection.MethodInfo ListRenderingExtensions = m_previewService.FieldType.GetMethod
                (
                    "ListRenderingExtensions",
                    Flags
                );


                object previewServiceInstance = m_previewService.GetValue(this.m_Viewer.LocalReport);

                System.Collections.IList extensions = ListRenderingExtensions.Invoke(previewServiceInstance, null) as System.Collections.IList;

                System.Reflection.PropertyInfo name = null;
                if (extensions.Count > 0)
                    name = extensions[0].GetType().GetProperty("Name", Flags);

                if (name == null)
                    return;

                // Debug info
                System.Collections.Generic.List<string> lsExtensions = new System.Collections.Generic.List<string>();

                foreach (object extension in extensions)
                {
                    string thisFormat = name.GetValue(extension, null).ToString();
                    lsExtensions.Add(thisFormat);

                    //{ 
                    //    System.Reflection.FieldInfo m_isVisible = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    //    System.Reflection.FieldInfo m_isExposedExternally = extension.GetType().GetField("m_isExposedExternally", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                    //    object valVisible = m_isVisible.GetValue(extension);
                    //    object valExposed = m_isExposedExternally.GetValue(extension);
                    //    System.Console.WriteLine(valVisible);
                    //    System.Console.WriteLine(valExposed);
                    //}

                    //if (string.Compare(thisFormat, formatName, true) == 0)
                    if (true)
                    {
                        System.Reflection.FieldInfo m_isVisible = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        System.Reflection.FieldInfo m_isExposedExternally = extension.GetType().GetField("m_isExposedExternally", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        m_isVisible.SetValue(extension, true);
                        m_isExposedExternally.SetValue(extension, true);
                        // break;
                    }

                } // Next extension

                // Debug info
                System.Console.WriteLine(lsExtensions);


            } // End if (m_previewService != null)

        } // End Sub EnableFormat 



        public Microsoft.Reporting.WebForms.ReportViewer m_Viewer;

        // Testing ReportViewer 2015
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Basic_SQL.SQL.GetSqlServerTypesDll(@"d:\");


            this.m_Viewer = new Microsoft.Reporting.WebForms.ReportViewer();
            EnableFormat("foo");


            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.WordOpenXml);
            // formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.Html);
            // formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.HtmlFragment);
            //formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.Image);
            formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.ExcelOpenXml);


            // this.m_Viewer.LocalReport.ReportPath = System.Web.Hosting.HostingEnvironment.MapPath("~/SimpleTest.rdl");

            using (System.IO.Stream reportDefinition = COR_Reports.ReportRepository.GetEmbeddedReport("SimpleTest.rdl"))
            {
                this.m_Viewer.LocalReport.LoadReportDefinition(reportDefinition);
            }


            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource();
            rds.Name = "DATA_Columns"; //This refers to the dataset name in the RDLC file
            string strSQL = @"SELECT TOP 100 * FROM information_schema.columns";
            // strSQL = strSQL.Replace("@_in_umzugsuid", "'" + in_ump_uid.Replace("'", "''") + "'");
            // strSQL = strSQL.Replace("@_in_sprache", "'" + in_sprache.Replace("'", "''") + "'");
            rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
            strSQL = null;

            this.m_Viewer.LocalReport.DataSources.Add(rds);


            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] result = this.m_Viewer.LocalReport.Render(formatInfo.FormatName, formatInfo.DeviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            string savePath = @"D:\";
            if (!System.StringComparer.OrdinalIgnoreCase.Equals(System.Environment.UserDomainName, "COR"))
                savePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);

            savePath = System.IO.Path.Combine(savePath, "SimpleTest" + formatInfo.Extension);
            System.IO.File.WriteAllBytes(savePath, result);
        } // End Sub Page_Load 


    } // End partial class _Default : System.Web.UI.Page


} // End Namespace ModifiedEmbedded2015
