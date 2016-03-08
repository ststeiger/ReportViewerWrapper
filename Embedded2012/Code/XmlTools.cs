
namespace COR_Reports
{


    class XmlTools
    {


        public static string XmlEscape(string unescaped)
        {
            string strReturnValue = null;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode node = doc.CreateElement("root");
            node.InnerText = unescaped;
            strReturnValue = node.InnerXml;
            node = null;
            doc = null;

            return strReturnValue;
        } // End Function XmlEscape


        public static string XmlUnescape(string escaped)
        {
            string strReturnValue = null;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode node = doc.CreateElement("root");
            node.InnerXml = escaped;
            strReturnValue = node.InnerText;
            node = null;
            doc = null;

            return strReturnValue;
        } // End Function XmlUnescape


        public static void SaveDocument(System.Xml.XmlDocument origDoc, System.IO.Stream strm, bool bDoReplace)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            
            if (bDoReplace)
                doc.LoadXml(origDoc.OuterXml.Replace("xmlns=\"\"", ""));
            
            using (System.Xml.XmlTextWriter xtw = new System.Xml.XmlTextWriter(strm, System.Text.Encoding.UTF8))
            {
                xtw.Formatting = System.Xml.Formatting.Indented; // if you want it indented
                xtw.Indentation = 4;
                xtw.IndentChar = ' ';

                doc.Save(xtw);
                xtw.Flush();
                xtw.Close();
            } // End Using xtw 

            doc = null;
        } // End Sub SaveDocument 


    } // End Class XmlTools 


} // End Namespace COR_Reports 
