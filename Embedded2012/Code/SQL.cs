
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


            csb.DataSource = "CORDB2008R2";
            csb.InitialCatalog = "Roomplanning";

            // csb.DataSource = "cordb2014";
            // csb.InitialCatalog = "ReportServer";


            csb.IntegratedSecurity = true;


            using (System.Data.Common.DbDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(strSQL, csb.ConnectionString))
            {
                da.Fill(dt);
            }

            return dt;
        }


        // DataSource must point to SQL-Server for <Microsoft.SqlServer.Types.dll-Version needed>
        public static void GetSqlServerTypesDll(string path)
        {
            System.Data.DataTable dt = Basic_SQL.SQL.GetDataTable(@"
SELECT 
	 af.name,
	 af.content 
FROM sys.assemblies a
INNER JOIN sys.assembly_files af ON a.assembly_id = af.assembly_id 
WHERE a.name = 'Microsoft.SqlServer.Types'     
    
");

            if (path != null && !path.ToLowerInvariant().EndsWith(".dll"))
                path = System.IO.Path.Combine(path, "Microsoft.SqlServer.Types.dll");


            byte[] ba = (byte[])dt.Rows[0]["content"];
            System.IO.File.WriteAllBytes(path, ba);
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