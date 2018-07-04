
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Embedded2017
{


    public partial class TestRender 
        : System.Web.UI.Page
    {


        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (this.Page.IsPostBack)
                return;


            // this.rptViewer.AsyncRendering = false;
            this.rptViewer.PageNavigation += myPageNavigation;

            Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource();
            rds.Name = "DataSet1"; //This refers to the dataset name in the RDLC file
            string strSQL = @"SELECT * FROM T_Benutzer";
            // strSQL = strSQL.Replace("@_in_umzugsuid", "'" + in_ump_uid.Replace("'", "''") + "'");
            // strSQL = strSQL.Replace("@_in_sprache", "'" + in_sprache.Replace("'", "''") + "'");
            rds.Value = Basic_SQL.SQL.GetDataTable(strSQL);
            strSQL = null;

            using (System.IO.Stream reportDefinition = COR_Reports.ReportRepository.GetEmbeddedReport("PaginationTest.rdl"))
            {
                this.rptViewer.LocalReport.LoadReportDefinition(reportDefinition);
            } // End Using reportDefinition 

            this.rptViewer.LocalReport.DataSources.Add(rds);
            // this.rptViewer.DataBind();

            this.rptViewer.CurrentPage = 3;

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //System.IO.StringWriter stringWriter = new System.IO.StringWriter(sb);
            //HtmlTextWriter htw = new HtmlTextWriter(stringWriter);
            
            //this.rptViewer.RenderControl(htw);
            //string foo = sb.ToString();
            //System.Console.WriteLine(foo);
            


            // this.rptViewer.LocalReport.ListRenderingExtensions();
            // this.rptViewer.LocalReport.Refresh();

            int a = this.rptViewer.LocalReport.GetTotalPages();


            // https://stackoverflow.com/questions/10585029/parse-an-html-string-with-js

            System.Console.WriteLine(a);

            Context.Response.Flush();

            // rs.CurrentPage = 3;
            // int b = rs.LocalReport.GetTotalPages();
        } // End Sub Page_Load 


        void myPageNavigation(object sender, Microsoft.Reporting.WebForms.PageNavigationEventArgs e)
        {
            System.Console.WriteLine(e.NewPage);
            //MessageBox.Show("CurrentPage will be" + e.NewPage);
        }

    } // End Class TestRender 


} // End Namespace Embedded2017 
