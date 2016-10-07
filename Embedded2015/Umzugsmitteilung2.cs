
using Basic_SQL;


namespace Portal_Reports
{


    public class Umzugsmitteilung2
    {


        // Pre: No value is NULL 
        // Post: output report bytes
        public static byte[] GetUmzugsmitteilung(COR_Reports.ReportFormatInfo formatInfo, string in_ump_uid, string in_sprache)
        {
            string report = "UM_Umzugsmitteilung2.rdl";
            byte[] baReport = null;

            // if (string.IsNullOrEmpty(in_ump_uid)) in_ump_uid = "C38CB749-1EEC-4686-9BBA-F627B9C4E8EC";
            if (string.IsNullOrEmpty(in_sprache)) in_sprache = "DE";


            // formatInfo = new Portal_Reports.ReportFormatInfo(ExportFormat.Word);


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
                    

                    viewer.SetParameters(lsParameters);
                    lsParameters.Clear();
                    lsParameters = null;

                    {
                        // Add data sources
                        COR_Reports.ReportDataSource rds = new COR_Reports.ReportDataSource();
                        rds.Name = "DataSet1"; //This refers to the dataset name in the RDLC file
                        string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rds.Name);
                        rds.Value = SQL.GetDataTable(strSQL);
                        
                        strSQL = null;
                        viewer.DataSources.Add(rds);
                    }

                    
                };

                baReport = COR_Reports.ReportTools.RenderReport(report, formatInfo, myFunc);
            }
            catch
            {
                // Basic_SQL.SQL.Log(ex);
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


    } // End Class Umzugsmitteilung


} // End Namespace Portal_Reports
