
// using COR_Reports;


namespace Planinfo_Bla
{


    public partial class _Default : System.Web.UI.Page
    {


        protected void Page_Load(object sender, System.EventArgs e)
        {
            // DependencyWalker.ViewDependencies();
            GetFooterPDF();
        } // End Sub Page_Load 
        

        public static void GetFooterPDF()
        {
            string ApertureDWG ="G00020-OG02_0000";
            ApertureDWG = "S0691_0000";
            GetFooterPDF(ApertureDWG);
        } // End Sub GetFooterPDF 


        // D:\reportviewerz\2005
        // Depends on TFS://COR-Library\COR_Reports\COR_Reports.csproj
        public static void GetFooterPDF(string ApertureDWG)
        {
            string report = "Planinfo.rdl";
            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.Html);


            COR_Reports.ReportTools.ReportDataCallback_t myFunc = delegate(COR_Reports.ReportViewer viewer, System.Xml.XmlDocument doc)
            {
                // viewer["format"] = formatInfo.FormatName;
                // viewer["extension"] = formatInfo.Extension;
                // viewer["report"] = report;

                //string extension = viewer["extension"];
                ////////////////////////////

                System.Collections.Generic.List<COR_Reports.ReportParameter> lsParameters =
                    new System.Collections.Generic.List<COR_Reports.ReportParameter>();
                
                lsParameters.Add(new COR_Reports.ReportParameter("in_aperturedwg", ApertureDWG));
                // lsParameters.Add(new COR_Reports.ReportParameter("datastart", "dateTimePickerStartRaport.Text"));
                // lsParameters.Add(new COR_Reports.ReportParameter("dataStop", "dateTimePickerStopRaport.Text"));

                viewer.SetParameters(lsParameters);
                lsParameters.Clear();
                lsParameters = null;


                // Add data sources
                COR_Reports.ReportDataSource rds = new COR_Reports.ReportDataSource();
                rds.Name = "DATA_Planinfo"; //This refers to the dataset name in the RDLC file
                string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rds.Name);
                strSQL = strSQL.Replace("@in_aperturedwg", "'" + ApertureDWG.Replace("'", "''") + "'");

                rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
                strSQL = null;
                viewer.DataSources.Add(rds);
            };


            byte[] baReport = COR_Reports.ReportTools.RenderReport(report, formatInfo, myFunc);

            using (System.IO.FileStream fs = System.IO.File.Create(@"D:\" + System.IO.Path.GetFileNameWithoutExtension(report) + formatInfo.Extension))
            {
                fs.Write(baReport, 0, baReport.Length);
            } // End Using fs

        } // End Sub GetFooterPDF 


    } // End Class _Default 


} // End Namespace Planinfo_Bla 
