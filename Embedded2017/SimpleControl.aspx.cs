using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Embedded2017
{
    public partial class SimpleControl : System.Web.UI.Page
    {

        public string RenderControlToHtml(Control controlToRender)
        {
            string retValue = null;

            controlToRender.Visible = true;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            using (System.IO.StringWriter stWriter = new System.IO.StringWriter(sb, System.Globalization.CultureInfo.InvariantCulture))
            {
                using (System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stWriter))
                {
                    controlToRender.DataBind();
                    controlToRender.RenderControl(htmlWriter);

                    htmlWriter.Flush();
                    stWriter.Flush();
                    retValue = sb.ToString();
                }
            }

            return retValue;
        }

        private static void EnableFormat(Microsoft.Reporting.WebForms.ReportViewer viewer, string formatName)
        {
            const System.Reflection.BindingFlags Flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;

            System.Type tt = viewer.LocalReport.GetType();
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


                object previewServiceInstance = m_previewService.GetValue(viewer.LocalReport);

                System.Collections.IList extensions = ListRenderingExtensions.Invoke(previewServiceInstance, null) as System.Collections.IList;

                System.Reflection.PropertyInfo name = null;
                if (extensions.Count > 0)
                    name = extensions[0].GetType().GetProperty("Name", Flags);

                if (name == null)
                    return;

                foreach (object extension in extensions)
                {
                    string thisFormat = name.GetValue(extension, null).ToString();

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
                        //break;
                    }

                } // Next extension

            } // End if (m_previewService != null)

        } // End Sub EnableFormat 


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
                return;

            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource();
            rds.Name = "DataSet1"; //This refers to the dataset name in the RDLC file
            string strSQL = @"SELECT * FROM T_Benutzer";
            // strSQL = strSQL.Replace("@_in_umzugsuid", "'" + in_ump_uid.Replace("'", "''") + "'");
            // strSQL = strSQL.Replace("@_in_sprache", "'" + in_sprache.Replace("'", "''") + "'");
            rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
            strSQL = null;


            Microsoft.Reporting.WebForms.ReportViewer rs = new Microsoft.Reporting.WebForms.ReportViewer();
            EnableFormat(rs, "HTML4.0");
            EnableFormat(rs, "IMAGE");

            using (System.IO.Stream reportDefinition = COR_Reports.ReportRepository.GetEmbeddedReport("PaginationTest.rdl"))
            {
                rs.LocalReport.LoadReportDefinition(reportDefinition);
            }

            rs.LocalReport.DataSources.Add(rds);


            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;


            //COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.HtmlFragment);
            COR_Reports.ReportFormatInfo formatInfo = new COR_Reports.ReportFormatInfo(COR_Reports.ExportFormat.Image);


            string deviceInfo =
  "<DeviceInfo>" +
  "  <OutputFormat>EMF</OutputFormat>" +
  "  <PageWidth>8.5in</PageWidth>" +
  "  <PageHeight>11in</PageHeight>" +
  "  <MarginTop>0.25in</MarginTop>" +
  "  <MarginLeft>0.25in</MarginLeft>" +
  "  <MarginRight>0.25in</MarginRight>" +
  "  <MarginBottom>0.25in</MarginBottom>" +
  "</DeviceInfo>";

            byte[] result = rs.LocalReport.Render(formatInfo.FormatName, formatInfo.DeviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
            System.Console.WriteLine(result);


            TestRender page = new TestRender();
            /*
            System.Web.UI.ScriptManager scriptManager = new ScriptManager();
            Page page = new Page();

            System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();
            form.Controls.Add(scriptManager);
            form.Controls.Add(rs);
            page.Controls.Add(form);
            // page.DataBind();  //exception here 
            */

            string s = RenderControlToHtml(page);
            System.Console.WriteLine(s);


        }
    }
}