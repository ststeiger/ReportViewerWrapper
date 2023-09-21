
namespace Embedded2017.SubReportExample
{


    // https://www.c-sharpcorner.com/article/rdlc-subreport-using-c-sharp-and-wpf/
    public class Group 
    {
        public int BG_ID { get; set; }

        public string BG_Name { get; set; }


        public int BEBG_BE { get; set; }


        // https://learn.microsoft.com/en-us/previous-versions/ms251783(v=vs.140)?redirectedfrom=MSDN
        public static void foo()
        {
            var employeeData = Employee.GetEmployees();
            var employees = new Microsoft.Reporting.WebForms.ReportDataSource() { Name = "EmployeeDetails", Value = employeeData };


            Microsoft.Reporting.WebForms.ReportViewer _reportViewer = new Microsoft.Reporting.WebForms.ReportViewer();
            _reportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

            
            _reportViewer.LocalReport.DataSources.Add(employees);


            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            path = System.IO.Path.Combine(path, "..", "..", "..", "Code", "SubReportExample", "Report1.rdl");
            path = System.IO.Path.GetFullPath(path);

            _reportViewer.LocalReport.ReportPath = path; // "/path/to/main/Report1.rdl";
            _reportViewer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
            _reportViewer.LocalReport.EnableExternalImages = true;


            // The ReportViewer control requires the report definitions for all subreports
            // be available before it can process a report.
            // If the local report was loaded from the file system by specifying the ReportPath property
            // or loaded from an embedded resource by specifying the ReportEmbeddedResource property,
            // the ReportViewer control automatically loads the subreports from the file system
            // or embedded resource, respectively.
            // In cases where the local report was loaded from a stream,
            // the LoadSubreportDefinition can be used to load report definitions for subreports.
            // _reportViewer.LocalReport.ReportEmbeddedResource = "ReportName"; // loads from embedded resources

            //using (System.IO.Stream strm = System.IO.File.OpenRead("path"))
            //{
            //    using (System.IO.TextReader tr = new System.IO.StreamReader(strm))
            //    {
            //        _reportViewer.LocalReport.LoadSubreportDefinition("reportName", tr);
            //    }
            //}

            // _reportViewer.SetDisplayMode(Microsoft.Reporting.WebForms.DisplayMode.PrintLayout);

            // _reportViewer.Dock = DockStyle.Fill;
            // this.Controls.Add(_reportViewer);

            // _reportViewer.Refresh();
            // _reportViewer.RefreshReport();

            _reportViewer.LocalReport.Refresh();
        }


        private static void LocalReport_SubreportProcessing(object sender, Microsoft.Reporting.WebForms.SubreportProcessingEventArgs e)
        {
            int ID = System.Convert.ToInt32(e.Parameters[0].Values[0]);
            var employeegroup = Group.GetGroups(); // .FindAll(x => x.ID == ID);
            if (e.ReportPath == "EmployeeDetails")
            {
                Microsoft.Reporting.WebForms.ReportDataSource employeeDetails = 
                    new Microsoft.Reporting.WebForms.ReportDataSource() { 
                    Name = "GroupsDetails", 
                    Value = employeegroup 
                };
                e.DataSources.Add(employeeDetails);
            }
        }

        public static System.Collections.Generic.IEnumerable<Group> GetGroups()
        {
            System.Collections.Generic.IEnumerable<Group> groups = new System.Collections.Generic.List<Group>()
           {
               new Group() {BG_ID = 1, BG_Name = "Applied Mathematics" },
               new Group() {BG_ID = 2, BG_Name = "Software" },
               new Group() {BG_ID = 3, BG_Name = "Machine Learning" },
               new Group() {BG_ID = 4, BG_Name = "Petroleum Engineering" },
           };
            return groups;
        }

    }
    
}
