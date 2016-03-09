
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;


namespace Embedded2015.Code
{


    // https://www.mssqltips.com/sqlservertip/3126/exporting-clr-assemblies-from-sql-server-back-to-dll-files/
    public class AssemblyExporter
    {

        
        [SqlProcedure]
        public static void SaveAssembly(string assemblyName, string destinationPath)
        {
            string sql = @"SELECT af.name, af.content FROM sys.assemblies a INNER JOIN sys.assembly_files af ON a.assembly_id = af.assembly_id WHERE a.name = @assemblyname";
            using (SqlConnection conn = new SqlConnection("context connection=true"))   //Create current context connection
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlParameter param = new SqlParameter("@assemblyname", System.Data.SqlDbType.NVarChar);
                    param.Value = assemblyName;
                    // param.Size = 128;
                    cmd.Parameters.Add(param);
                    cmd.Connection.Open();  //Open the context connetion
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) //Iterate through assembly files
                        {
                            string assemblyFileName = reader.GetString(0);  //get assembly file name from the name (first) column
                            System.Data.SqlTypes.SqlBytes bytes = reader.GetSqlBytes(1);         //get assembly binary data from the content (second) column
                            string outputFile = System.IO.Path.Combine(destinationPath, assemblyFileName);
                            SqlContext.Pipe.Send(string.Format("Exporting assembly file [{0}] to [{1}]", assemblyFileName, outputFile)); //Send information about exported file back to the calling session
                            using (System.IO.FileStream byteStream = new System.IO.FileStream(outputFile, System.IO.FileMode.CreateNew))
                            {
                                byteStream.Write(bytes.Value, 0, (int)bytes.Length);
                                byteStream.Close();
                            }
                        }
                    }
                }
                conn.Close();
            }
        } // End Sub SaveAssembly 
        

        public static void SaveAssembly2(string assemblyName, string destinationPath)
        {
            string sql = @"SELECT af.name, af.content FROM sys.assemblies a INNER JOIN sys.assembly_files af ON a.assembly_id = af.assembly_id WHERE a.name = @assemblyname";


            using (System.Data.Common.DbConnection conn = new System.Data.SqlClient.SqlConnection("context connection=true"))   //Create current context connection
            {
                using (System.Data.Common.DbCommand cmd = new System.Data.SqlClient.SqlCommand(sql, (System.Data.SqlClient.SqlConnection)conn))
                {
                    System.Data.Common.DbParameter param = new System.Data.SqlClient.SqlParameter("@assemblyname", System.Data.SqlDbType.NVarChar);
                    param.Value = assemblyName;
                    // param.Size = 128;
                    cmd.Parameters.Add(param);


                    using (System.IO.Stream fs = new System.IO.FileStream("logo" + "pub_id" + ".bmp", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write))
                    {
                        using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
                        {
                            long startIndex = 0;
                            var buffer = new byte[1024];
                            int bufferSize = buffer.Length;

                            if(cmd.Connection.State != System.Data.ConnectionState.Open)
                                cmd.Connection.Open();  //Open the context connetion

                            using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read()) //Iterate through assembly files
                                {
                                    string assemblyFileName = reader.GetString(0); //get assembly file name from the name (first) column

                                    long retval = reader.GetBytes(1, startIndex, buffer, 0, bufferSize);

                                    // Continue reading and writing while there are bytes beyond the size of the buffer.
                                    while (retval == bufferSize)
                                    {
                                        bw.Write(buffer);
                                        bw.Flush();

                                        // Reposition the start index to the end of the last buffer and fill the buffer.
                                        startIndex += bufferSize;
                                        retval = reader.GetBytes(1, startIndex, buffer, 0, bufferSize);
                                    } // Whend

                                    // Write the remaining buffer.
                                    bw.Write(buffer, 0, (int)retval);
                                    bw.Flush();
                                    bw.Close();
                                } // Whend reader.Read

                            } // End Using reader 

                        } // End using bw

                        fs.Flush();
                        fs.Close();
                    } // End using fs

                } // End using cmd 

                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            } // End Using conn 

        } // End Sub SaveAssembly2 



        // http://stackoverflow.com/questions/2885335/clr-sql-assembly-get-the-bytestream
        // http://stackoverflow.com/questions/891617/how-to-read-a-image-by-idatareader
        // http://stackoverflow.com/questions/4103406/extracting-a-net-assembly-from-sql-server-2005
        public virtual void SaveAssembly3(string assemblyName, string path)
        {
            string sql = @"
--DECLARE @__assemblyname nvarchar(260)
--SET @__assemblyname = 'Microsoft.SqlServer.Types'


SELECT 
	 A.name
	,AF.content 
FROM sys.assembly_files AS AF 

INNER JOIN sys.assemblies AS A 
	ON AF.assembly_id = A.assembly_id 
	
WHERE AF.file_id = 1 
AND A.name = @__assemblyname
;

";

            using (System.Data.IDbConnection conn = new System.Data.SqlClient.SqlConnection("context connection=true"))   //Create current context connection
            {

                using (System.Data.IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    System.Data.IDbDataParameter param = cmd.CreateParameter();
                    param.ParameterName = "@__assemblyname";
                    param.DbType = System.Data.DbType.String;
                    param.Value = assemblyName;
                    param.Size = 128;
                    cmd.Parameters.Add(param);
                    cmd.Prepare();


                    using (System.Data.IDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        const int BUFFER_SIZE = 1024;
                        byte[] buffer = new byte[BUFFER_SIZE];

                        int col = reader.GetOrdinal("content");
                        int bytesRead = 0;
                        int offset = 0;

                        // write the byte stream out to disk
                        using (System.IO.FileStream bytestream = new System.IO.FileStream(path, System.IO.FileMode.CreateNew))
                        {

                            while ((bytesRead = (int)reader.GetBytes(col, offset, buffer, 0, BUFFER_SIZE)) > 0)
                            {
                                bytestream.Write(buffer, 0, bytesRead);
                                offset += bytesRead;
                            } // Whend

                            bytestream.Close();
                        } // End Using bytestream 

                        reader.Close();
                    } // End Using reader

                } // End Using cmd

            } // End Using conn 


        } // End Function SaveAssembly3


    } // End Class AssemblyExporter


} // End Namespace Embedded2015.Code
