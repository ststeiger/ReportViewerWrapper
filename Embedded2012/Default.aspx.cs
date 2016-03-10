
namespace Embedded2012
{


    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // FSCK ! Microsoft.SqlServer.Types Version 11 ! 
            // COR_Reports.DependencyWalker.ViewDependencies();

            // GetPDF();
            // GetFooterPDF();

            GetLegends();

            // this.litContent.Text = GetFooterHtmlFragment();
        } // End Sub Page_Load 


        public static byte[] GetFooterPDF()
        {
            string in_stylizer = "REM Bodenbelag";
            string in_aperturedwg = "G00020-OG02_0000";
            in_aperturedwg = "S0691_0000";
            in_aperturedwg = "G00020-OG02_0000";

            string report = "Planinfo_StadtZuerich.rdl";

            //COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.PDF);
            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.WordOpenXml);

            // byte[] foot = GetFooter(report, formatInfo, in_aperturedwg, in_stylizer);

            byte[] foot = GetUmzugsmitteilung("UM_Umzugsmitteilung.rdl", formatInfo, "C38CB749-1EEC-4686-9BBA-F627B9C4E8EC", "DE");
            // System.IO.File.WriteAllBytes(@"d:\Test_UM_Umzugsmitteilung" + formatInfo.Extension, foot);

            return foot;
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
            if (baReport != null)
                retVal = System.Text.Encoding.UTF8.GetString(baReport);

            return retVal;
        } // End Sub GetFooterHtmlFragment 




        public static byte[] GetLegends()
        {
            string in_stylizer = "REM Bodenbelag";
            string in_aperturedwg = "G00020-OG02_0000";
            in_aperturedwg = "S0691_0000";
            in_aperturedwg = "G00020-OG02_0000";

            string report = "LegendenDesign2008.rdl";

            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.PDF);
            return GetLegends(report, formatInfo, in_aperturedwg, in_stylizer);
        } // End Sub GetFooterPDF 


        // D:\reportviewerz\2005
        // Depends on TFS://COR-Library\COR_Reports\COR_Reports.csproj
        // Pre: No value is NULL 
        // Post: output report bytes
        public static byte[] GetLegends(string report, COR_Reports.ReportFormatInfo formatInfo, string in_aperturedwg, string in_stylizer)
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


                    lsParameters.Add(new COR_Reports.ReportParameter("in_sprache", "DE"));
                    // lsParameters.Add(new COR_Reports.ReportParameter("in_aperturedwg", in_aperturedwg));
                    // lsParameters.Add(new COR_Reports.ReportParameter("in_stylizer", in_stylizer));
                    viewer.SetParameters(lsParameters);

                    
                    lsParameters.Clear();
                    lsParameters = null;

                    // Add data sources
                    // DATA_Legenden'
                    {
                        COR_Reports.ReportDataSource rds = new COR_Reports.ReportDataSource();
                        rds.Name = "DATA_Legenden"; //This refers to the dataset name in the RDLC file
                        string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rds.Name);
                        // strSQL = strSQL.Replace("@in_aperturedwg", "'" + in_aperturedwg.Replace("'", "''") + "'");
                        // strSQL = strSQL.Replace("@in_stylizer", "'" + in_stylizer.Replace("'", "''") + "'");

                        rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
                        strSQL = null;
                        viewer.DataSources.Add(rds);
                    }



                    // DATA_Layersetinfo
                    {
                        COR_Reports.ReportDataSource rds = new COR_Reports.ReportDataSource();
                        rds.Name = "DATA_Layersetinfo"; //This refers to the dataset name in the RDLC file
                        string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rds.Name);
                        // strSQL = strSQL.Replace("@in_aperturedwg", "'" + in_aperturedwg.Replace("'", "''") + "'");
                        // strSQL = strSQL.Replace("@in_stylizer", "'" + in_stylizer.Replace("'", "''") + "'");

                        rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
                        strSQL = null;
                        viewer.DataSources.Add(rds);
                    }


                    // DATA_LayersetFelder
                    {
                        COR_Reports.ReportDataSource rds = new COR_Reports.ReportDataSource();
                        rds.Name = "DATA_LayersetFelder"; //This refers to the dataset name in the RDLC file
                        string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rds.Name);
                        // strSQL = strSQL.Replace("@in_aperturedwg", "'" + in_aperturedwg.Replace("'", "''") + "'");
                        // strSQL = strSQL.Replace("@in_stylizer", "'" + in_stylizer.Replace("'", "''") + "'");

                        rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
                        strSQL = null;
                        viewer.DataSources.Add(rds);
                    }


                }; // End Sub ReportDataCallback_t 

                baReport = COR_Reports.ReportTools.RenderReport(report, formatInfo, myFunc);

            }
            catch (System.Exception ex)
            {
                Basic_SQL.SQL.Log(ex);
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
        } // End Sub GetLegends 


        // Pre: No value is NULL 
        // Post: output report bytes
        public static byte[] GetUmzugsmitteilung(string report, COR_Reports.ReportFormatInfo formatInfo, string in_ump_uid, string in_sprache)
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


                    lsParameters.Add(new COR_Reports.ReportParameter("in_sprache", in_sprache));
                    lsParameters.Add(new COR_Reports.ReportParameter("in_umzugsuid", in_ump_uid));
                    // lsParameters.Add(new COR_Reports.ReportParameter("datastart", "dateTimePickerStartRaport.Text"));
                    // lsParameters.Add(new COR_Reports.ReportParameter("dataStop", "dateTimePickerStopRaport.Text"));

                    viewer.SetParameters(lsParameters);
                    lsParameters.Clear();
                    lsParameters = null;

                    {
                        // Add data sources
                        COR_Reports.ReportDataSource rds = new COR_Reports.ReportDataSource();
                        rds.Name = "DATA_Umzugsmitteilung"; //This refers to the dataset name in the RDLC file
                        string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rds.Name);
                        strSQL = strSQL.Replace("@_in_umzugsuid", "'" + in_ump_uid.Replace("'", "''") + "'");
                        strSQL = strSQL.Replace("@_in_sprache", "'" + in_sprache.Replace("'", "''") + "'");
                        rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
                        strSQL = null;
                        viewer.DataSources.Add(rds);
                    }


                    {
                        COR_Reports.ReportDataSource rdsHeader = new COR_Reports.ReportDataSource();
                        rdsHeader.Name = "DATA_Umzugsheader"; //This refers to the dataset name in the RDLC file
                        string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rdsHeader.Name);
                        strSQL = strSQL.Replace("@_in_umzugsuid", "'" + in_ump_uid.Replace("'", "''") + "'");
                        rdsHeader.Value = Basic_SQL.SQL.GetDataTable(strSQL);
                        strSQL = null;
                        viewer.DataSources.Add(rdsHeader);
                    }

                    {
                        COR_Reports.ReportDataSource rdsTranslation = new COR_Reports.ReportDataSource();
                        rdsTranslation.Name = "DATA_Report_Translation"; //This refers to the dataset name in the RDLC file
                        string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rdsTranslation.Name);
                        strSQL = strSQL.Replace("@in_sprache", "'" + in_sprache.Replace("'", "''") + "'");
                        rdsTranslation.Value = Basic_SQL.SQL.GetDataTable(strSQL);
                        strSQL = null;
                        viewer.DataSources.Add(rdsTranslation);
                    }

                }; // End Sub ReportDataCallback_t 

                baReport = COR_Reports.ReportTools.RenderReport(report, formatInfo, myFunc);

            }
            catch (System.Exception ex)
            {
                Basic_SQL.SQL.Log(ex);
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
        } // End Sub GetUmzugsmitteilung 



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
                }; // End Sub ReportDataCallback_t 

                baReport = COR_Reports.ReportTools.RenderReport(report, formatInfo, myFunc);

            }
            catch (System.Exception ex)
            {
                Basic_SQL.SQL.Log(ex);
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
        } // End Sub GetFooter 


        public static byte[] GetPDF()
        {
            string report = "Report2.rdl";

            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.PDF);
            return GetReport2(report, formatInfo);
        } // End Sub GetFooterPDF 


        // D:\reportviewerz\2005
        // Depends on TFS://COR-Library\COR_Reports\COR_Reports.csproj
        // Pre: No value is NULL 
        // Post: output report bytes
        public static byte[] GetReport2(string report, COR_Reports.ReportFormatInfo formatInfo)
        {
            byte[] baReport = null;

            try
            {
                COR_Reports.ReportTools.ReportDataCallback_t myFunc = delegate(COR_Reports.ReportViewer viewer, System.Xml.XmlDocument doc)
                {
                    // Add data sources
                    COR_Reports.ReportDataSource rds = new COR_Reports.ReportDataSource();
                    rds.Name = "DataSet1"; //This refers to the dataset name in the RDLC file
                    string strSQL = COR_Reports.ReportTools.GetDataSetDefinition(doc, rds.Name);


                    rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
                    strSQL = null;
                    viewer.DataSources.Add(rds);
                }; // End Sub ReportDataCallback_t 

                baReport = COR_Reports.ReportTools.RenderReport(report, formatInfo, myFunc);

            }
            catch (System.Exception ex)
            {
                Basic_SQL.SQL.Log(ex);
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
        } // End Sub GetReport2 


    } // End partial class _Default : System.Web.UI.Page


} // End Namespace Embedded2012
