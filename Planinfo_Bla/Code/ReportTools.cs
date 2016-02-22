
namespace COR_Reports
{


    public class ReportTools
    {


        public static System.Xml.XmlNamespaceManager GetReportNamespaceManager(System.Xml.XmlDocument doc)
        {
            if (doc == null)
                throw new System.ArgumentNullException("doc");

            System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);

            // <Report xmlns="http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition" xmlns:rd="http://schemas.microsoft.com/SQLServer/reporting/reportdesigner">

            if (doc.DocumentElement != null)
            {
                // string strNamespace = doc.DocumentElement.NamespaceURI;
                // System.Console.WriteLine(strNamespace);
                // nsmgr.AddNamespace("dft", strNamespace);

                System.Xml.XPath.XPathNavigator xNav = doc.CreateNavigator();
                while (xNav.MoveToFollowing(System.Xml.XPath.XPathNodeType.Element))
                {
                    System.Collections.Generic.IDictionary<string, string> localNamespaces = 
                        xNav.GetNamespacesInScope(System.Xml.XmlNamespaceScope.Local);

                    foreach (System.Collections.Generic.KeyValuePair<string, string> kvp in localNamespaces)
                    {
                        string prefix = kvp.Key;
                        if (string.IsNullOrEmpty(prefix))
                            prefix = "dft";

                        nsmgr.AddNamespace(prefix, kvp.Value);
                    } // Next kvp

                } // Whend

                return nsmgr;
            } // End if (doc.DocumentElement != null)

            nsmgr.AddNamespace("dft", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
            // nsmgr.AddNamespace("dft", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");

            return nsmgr;
        } // End Function GetReportNamespaceManager


        public static bool HasDataSet(System.Xml.XmlDocument doc, string dataSetName)
        {
            dataSetName = XmlTools.XmlEscape(dataSetName);

            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            System.Xml.XmlNode xnProc = doc.SelectSingleNode("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"" + dataSetName + "\"]", nsmgr);

            return xnProc != null;
        } // End Function HasDataSet


        public static string GetDataSetDefinition(System.Xml.XmlDocument doc, string dataSetName)
        {
            dataSetName = XmlTools.XmlEscape(dataSetName);

            if (HasDataSet(doc, dataSetName))
            {
                System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
                System.Xml.XmlNode xnSQL = doc.SelectSingleNode("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"" + dataSetName + "\"]/dft:Query/dft:CommandText", nsmgr);
                return xnSQL.InnerText;
            }

            return null;
        } // End Function GetDataSetDefinition



        public delegate void ReportDataCallback_t(ReportViewer viewer, System.Xml.XmlDocument rep);



        public static byte[] RenderReport(string report, ReportFormatInfo formatInfo, ReportDataCallback_t cbGetReportDetails)
        {
            byte[] baReport = null;

            using (ReportViewer viewer = new ReportViewer())
            {
                // viewer.ReportPath = System.Web.Hosting.HostingEnvironment.MapPath("~/" + report);
                viewer.LoadEmbeddedReportDefinition(report);

                System.Xml.XmlDocument rep = ReportRepository.GetReport(report);

                if (cbGetReportDetails != null)
                    cbGetReportDetails(viewer, rep);

                rep = null;

                baReport = viewer.Render(formatInfo);
            } // End Using MyReportViewer

            return baReport;
        } // End Function RenderReport 


    } // End Class ReportTools 


} // End Namespace COR_Reports 
