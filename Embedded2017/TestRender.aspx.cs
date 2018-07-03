
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
            int a = this.rptViewer.LocalReport.GetTotalPages();
            System.Console.WriteLine(a);

            // rs.CurrentPage = 3;
            // int b = rs.LocalReport.GetTotalPages();
        } // End Sub Page_Load 


    } // End Class TestRender 


} // End Namespace Embedded2017 
