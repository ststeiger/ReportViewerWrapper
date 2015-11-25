
namespace Planinfo_Bla
{


    public partial class _Default : System.Web.UI.Page
    {


        protected void Page_Load(object sender, System.EventArgs e)
        {
            // DependencyWalker.ViewDependencies();
            this.litContent.Text = GetFooterHtmlFragment();
        } // End Sub Page_Load 
        

        public static byte[] GetFooterPDF()
        {
            string in_stylizer = "REM Bodenbelag";
            string in_aperturedwg = "G00020-OG02_0000";
            in_aperturedwg = "S0691_0000";
            in_aperturedwg = "G00020-OG02_0000";

            string report = "Planinfo_StadtZuerich.rdl";
            
            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.PDF);
            return GetFooter(report, formatInfo, in_aperturedwg, in_stylizer);
        } // End Sub GetFooterPDF 


        public static string GetFooterHtmlFragment()
        {
            string retVal = "";
            string in_stylizer = "REM Bodenbelag";
            string in_aperturedwg = "G00020-OG02_0000";
            in_aperturedwg = "S0691_0000";
            in_aperturedwg = "G00020-OG02_0000";

            string report = "Planinfo_StadtZuerich.rdl";
            
            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.HtmlFragment);
            byte[] baReport = GetFooter(report, formatInfo, in_aperturedwg, in_stylizer);
            if(baReport != null)
                retVal = System.Text.Encoding.UTF8.GetString(baReport);

            return retVal;
        }


        // D:\reportviewerz\2005
        // Depends on TFS://COR-Library\COR_Reports\COR_Reports.csproj
        // Pre: No value is NULL 
        // Post: output report bytes
        public static byte[] GetFooter(string report, COR_Reports.ReportFormatInfo formatInfo, string in_aperturedwg, string in_stylizer)
        {
            byte[] baReport = null;
            
            try
            {
                COR_Reports.ReportTools.ReportDataCallback_t myFunc = delegate(COR_Reports.ReportViewer viewer, System.Xml.XmlDocument doc)
                {
                    // viewer["format"] = formatInfo.FormatName;
                    // viewer["extension"] = formatInfo.Extension;
                    // viewer["report"] = report;

                    //string extension = viewer["extension"];
                    ////////////////////////////

                    System.Collections.Generic.List<COR_Reports.ReportParameter> lsParameters =
                        new System.Collections.Generic.List<COR_Reports.ReportParameter>();

                    lsParameters.Add(new COR_Reports.ReportParameter("in_aperturedwg", in_aperturedwg));
                    lsParameters.Add(new COR_Reports.ReportParameter("in_stylizer", in_stylizer));
                    // lsParameters.Add(new COR_Reports.ReportParameter("datastart", "dateTimePickerStartRaport.Text"));
                    // lsParameters.Add(new COR_Reports.ReportParameter("dataStop", "dateTimePickerStopRaport.Text"));

                    viewer.SetParameters(lsParameters);
                    lsParameters.Clear();
                    lsParameters = null;

                    // Add data sources
                    COR_Reports.ReportDataSource rds = new COR_Reports.ReportDataSource();
                    rds.Name = "DATA_Planinfo"; //This refers to the dataset name in the RDLC file
                    string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rds.Name);
                    strSQL = strSQL.Replace("@in_aperturedwg", "'" + in_aperturedwg.Replace("'", "''") + "'");
                    strSQL = strSQL.Replace("@in_stylizer", "'" + in_stylizer.Replace("'", "''") + "'");

                    rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
                    strSQL = null;
                    viewer.DataSources.Add(rds);
                };

                baReport = COR_Reports.ReportTools.RenderReport(report, formatInfo, myFunc);

            }
            catch (System.Exception ex)
            {
                Log(ex);
                throw;
            }


            // If testing
            if (System.StringComparer.InvariantCultureIgnoreCase.Equals(System.Environment.UserDomainName, "COR"))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(@"D:\" + System.IO.Path.GetFileNameWithoutExtension(report) + formatInfo.Extension))
                {
                    fs.Write(baReport, 0, baReport.Length);
                } // End Using fs
            }
            
            return baReport;
        } // End Sub GetFooterPDF 


        private static void Log(System.Exception ex)
        {
            System.Console.WriteLine(ex);
        } // End Sub Log 


    } // End Class _Default 


} // End Namespace Planinfo_Bla 
