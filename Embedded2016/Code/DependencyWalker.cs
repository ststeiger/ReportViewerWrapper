
namespace COR_Reports
{


    public class DependencyWalker
    {


        public static string GetAssemblyDirectory(System.Reflection.Assembly ass)
        {
            string codeBase = ass.CodeBase;
            System.UriBuilder uri = new System.UriBuilder(codeBase);
            string path = System.Uri.UnescapeDataString(uri.Path);
            //return System.IO.Path.GetDirectoryName(path);
            return path;
        } // End Function GetAssemblyDirectory


        public static void GetDependencies(System.Reflection.Assembly ass, 
            System.Collections.Generic.List<System.Reflection.Assembly> ls,
            System.Collections.Generic.List<string> paths, string name)
        {
            name += "/" + ass.GetName().Name;
            paths.Add(name);

            //if (StringComparer.OrdinalIgnoreCase.Equals(ass.GetName().Name, "Microsoft.ReportViewer.ProcessingObjectModel"))
            if (true)
            {
                string assemblyPath = GetAssemblyDirectory(ass);
                System.Console.WriteLine(assemblyPath);
                string assemblyFileName = System.IO.Path.GetFileName(assemblyPath);

                string assemblyOutputDirectory = @"d:\reportviewerz\";
                if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                    assemblyOutputDirectory = "/root/reportviewerz/";

                if (!System.IO.Directory.Exists(assemblyOutputDirectory))
                    System.IO.Directory.CreateDirectory(assemblyOutputDirectory);

                System.IO.File.Copy(assemblyPath, assemblyOutputDirectory + assemblyFileName, true);
            } // End if 



            System.Reflection.AssemblyName[] asmNames = ass.GetReferencedAssemblies();

            foreach (System.Reflection.AssemblyName asmn in asmNames)
            {
                System.Reflection.Assembly ass2 = System.Reflection.Assembly.Load(asmn);

                if (!ls.Contains(ass2))
                {
                    ls.Add(ass2);
                    GetDependencies(ass2, ls, paths, name);
                    //ls.AddRange(GetDependencies(ass2));
                } // End if (!ls.Contains(ass2)) 

            } // Next asmn 

        } // End Sub GetDependencies


        public static void ViewDependencies()
        {
            System.Reflection.Assembly ass = typeof(Microsoft.Reporting.WebForms.ReportViewer).Assembly;
            string name = "";
            // ass = System.Reflection.Assembly.GetExecutingAssembly();

            System.Collections.Generic.List<System.Reflection.Assembly> ls = 
                new System.Collections.Generic.List<System.Reflection.Assembly>();
            System.Collections.Generic.List<string> paths =
                new System.Collections.Generic.List<string>();
            GetDependencies(ass, ls, paths, name);
            
            System.Console.WriteLine(paths);
            System.Console.WriteLine(ls);
        } // End Sub ViewDependencies 


    } // End Class DependencyWalker 


} // End Namespace Planinfo_Bla 
