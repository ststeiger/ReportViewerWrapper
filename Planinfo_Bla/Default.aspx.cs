
namespace Planinfo_Bla
{


    public partial class _Default : System.Web.UI.Page
    {


        protected void Page_Load(object sender, System.EventArgs e)
        {
            // DependencyWalker.ViewDependencies();
            GetFooterPDF();
        } // End Sub Page_Load 
        

        public static byte[] GetFooterPDF()
        {
            string ApertureDWG ="G00020-OG02_0000";
            ApertureDWG = "S0691_0000";
            ApertureDWG = "G00020-OG02_0000";

            string report = "Planinfo_StadtZuerich.rdl";
            string stylizer = "REM Bodenbelag";

            return GetFooterPDF(report, ApertureDWG, stylizer);
        } // End Sub GetFooterPDF 


        // D:\reportviewerz\2005
        // Depends on TFS://COR-Library\COR_Reports\COR_Reports.csproj
        // Pre: No value is NULL 
        // Post: output report bytes
        public static byte[] GetFooterPDF(string report, string in_aperturedwg, string in_stylizer)
        {
            byte[] baReport = null;
            //COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.PDF);
            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.Html);

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

            using (System.IO.FileStream fs = System.IO.File.Create(@"D:\" + System.IO.Path.GetFileNameWithoutExtension(report) + formatInfo.Extension))
            {
                fs.Write(baReport, 0, baReport.Length);
            } // End Using fs

            return baReport;
        } // End Sub GetFooterPDF 


        private static void Log(System.Exception ex)
        {
            System.Console.WriteLine(ex);
        } // End Sub Log 


    } // End Class _Default 


} // End Namespace Planinfo_Bla 
