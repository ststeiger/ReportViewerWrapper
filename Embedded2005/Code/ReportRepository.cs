
namespace COR_Reports
{


    public class ReportRepository
    {


        public static System.IO.Stream GetEmbeddedReport(string name)
        {
            System.Reflection.Assembly ass = typeof(ReportRepository).Assembly;
            string resourceName = null;

            foreach (string thisResourceName in ass.GetManifestResourceNames())
            {

                if (thisResourceName.EndsWith(name, System.StringComparison.InvariantCultureIgnoreCase))
                {
                    resourceName = thisResourceName;
                    break;
                } // End if (thisResourceName.EndsWith(name, System.StringComparison.InvariantCultureIgnoreCase)) 

            } // Next thisResourceName

            return ass.GetManifestResourceStream(resourceName);
        } // End Function GetEmbeddedReport 


        public static System.Xml.XmlDocument GetReport(string reportName)
        {
            // http://blogs.msdn.com/b/tolong/archive/2007/11/15/read-write-xml-in-memory-stream.aspx
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            // doc.Load(memorystream);
            // doc.Load(FILE_NAME);
            // doc.Load(strFileName);


            using (System.IO.Stream strm = GetEmbeddedReport(reportName))
            {

                using (System.Xml.XmlTextReader xtrReader = new System.Xml.XmlTextReader(strm))
                {
                    doc.Load(xtrReader);
                    xtrReader.Close();
                } // End Using xtrReader

            } // End Using strm 

            return doc;
        } // End Function GetReport


    } // End Class ReportRepository 


} // End Namespace COR_Reports 
