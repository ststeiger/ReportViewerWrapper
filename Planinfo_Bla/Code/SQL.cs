
namespace Basic_SQL
{


    public class SQL
    {



        public static System.Data.DataTable GetDataTable(string strSQL)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();

            csb.DataSource = System.Environment.MachineName;
            csb.DataSource = @"VMSTZHDB08\SZH_DBH_1";
            csb.InitialCatalog = "HBD_CAFM_V3";

            csb.IntegratedSecurity = true;


            using (System.Data.Common.DbDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(strSQL, csb.ConnectionString))
            {
                da.Fill(dt);
            }

            return dt;
        }


        private static void OtherWaysToGetReport()
        {
            string report = @"d:\bla.rdl";

            // string lalal = System.IO.File.ReadAllText(report);
            // byte[] foo = System.Text.Encoding.UTF8.GetBytes(lalal);
            // byte[] foo = System.IO.File.ReadAllBytes(report);

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {

                using (System.IO.FileStream file = new System.IO.FileStream(report, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                    ms.Flush();
                    ms.Position = 0;
                }


                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("resource"))
                {
                    using (System.IO.TextReader reader = new System.IO.StreamReader(ms))
                    {
                        // rv.LocalReport.LoadReportDefinition(reader);
                    }
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(report))
                {
                    // rv.LocalReport.LoadReportDefinition(reader);
                }


            }
        }


        public static void Log(System.Exception ex)
        {
            System.Console.WriteLine(ex);
        } // End Sub Log 


    }


}