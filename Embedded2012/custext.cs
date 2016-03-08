
#if CANNOT_BE_DONE_WITH_LOCALREPORT_BECAUSE_MICROSOFT_WANTS_YOU_TO_BUY_SQL_SERVER


// Not possible with ReportViewer...
namespace WordDocumentPortal
{

    // https://msdn.microsoft.com/en-us/library/microsoft.reportingservices.ondemandreportrendering.irenderingextension(v=sql.110).aspx
    // https://blogs.msdn.microsoft.com/bryanke/2004/03/16/how-hard-is-it-to-write-reporting-services-extensions/

    // https://stackoverflow.com/questions/4880222/modifying-pdf-rendered-by-reporting-services-before-sending-it-to-the-client


    // http://www.codeproject.com/Articles/488883/Zip-Rendering-Extension-for-SQL-Server-Reporting-S
    // http://www.codeproject.com/Articles/23966/Report-Viewer-generate-reports-MS-Word-formats


    // IRenderingExtension Interface
    // Namespace:  Microsoft.ReportingServices.OnDemandReportRendering
    // Assembly:  Microsoft.ReportingServices.ProcessingCore (in Microsoft.ReportingServices.ProcessingCore.dll)
    public class custext
        : Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension
    {
        /*
        public void GetRenderingResource(Microsoft.ReportingServices.Interfaces.CreateAndRegisterStream createAndRegisterStreamCallback, System.Collections.Specialized.NameValueCollection deviceInfo)
        {
            throw new NotImplementedException();
        }

        public bool Render(Microsoft.ReportingServices.OnDemandReportRendering.Report report, System.Collections.Specialized.NameValueCollection reportServerParameters, System.Collections.Specialized.NameValueCollection deviceInfo, System.Collections.Specialized.NameValueCollection clientCapabilities, ref System.Collections.Hashtable renderProperties, Microsoft.ReportingServices.Interfaces.CreateAndRegisterStream createAndRegisterStream)
        {
            throw new NotImplementedException();
        }

        public bool RenderStream(string streamName, Microsoft.ReportingServices.OnDemandReportRendering.Report report, System.Collections.Specialized.NameValueCollection reportServerParameters, System.Collections.Specialized.NameValueCollection deviceInfo, System.Collections.Specialized.NameValueCollection clientCapabilities, ref System.Collections.Hashtable renderProperties, Microsoft.ReportingServices.Interfaces.CreateAndRegisterStream createAndRegisterStream)
        {
            throw new NotImplementedException();
        }

        public string LocalizedName
        {
            get { throw new NotImplementedException(); }
        }

        public void SetConfiguration(string configuration)
        {
            throw new NotImplementedException();
        }
         * */




        // https://stackoverflow.com/questions/4880222/modifying-pdf-rendered-by-reporting-services-before-sending-it-to-the-client
        // http://www.codeproject.com/Articles/488883/Zip-Rendering-Extension-for-SQL-Server-Reporting-S
        public class xxx
        {

            //Stream to which the intermediate report will be rendered
            private System.IO.Stream intermediateStream;
            private string _name;
            private string _extension;
            private System.Text.Encoding _encoding;
            private string _mimeType;
            private bool _willSeek;
            private Microsoft.ReportingServices.Interfaces.StreamOper _operation;



            class SomeOtherTypeInSameAssembly
            { 
                // TODO: ALL INTERNAL there...
            }

            class TestMe
            {
                public string X { get; set; }

                TestMe(string x)
                {
                    this.X = x;
                }
            }

            // https://stackoverflow.com/questions/2023193/c-sharp-instantiating-internal-class-with-private-constructor
            // https://stackoverflow.com/questions/440016/activator-createinstance-with-private-sealed-class
            public void test()
            {
                //Microsoft.ReportingServices.Rendering.HtmlRenderer.
                //var xxx = new Microsoft.ReportingServices.Rendering.ImageRenderer.

                System.Type type = typeof(TestMe);


                // System.Type t = typeof(SomeOtherTypeInSameAssembly).Assembly.GetType("Microsoft.ReportingServices.Rendering.ImageRenderer.PDFRenderer");

                System.Reflection.ConstructorInfo c = type.GetConstructor(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                    null, new System.Type[] { typeof(string) }, null);

                object o = c.Invoke(new object[] { "foo" });


            }
        }



        //Stream to which the intermediate report will be rendered
        private System.IO.Stream intermediateStream;
        private string _name;
        private string _extension;
        private System.Text.Encoding _encoding;
        private string _mimeType;
        private bool _willSeek;
        private Microsoft.ReportingServices.Interfaces.StreamOper _operation;



        void Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.GetRenderingResource(Microsoft.ReportingServices.Interfaces.CreateAndRegisterStream createAndRegisterStreamCallback, System.Collections.Specialized.NameValueCollection deviceInfo)
        {
            throw new System.NotImplementedException();
        }


        //New implementation of the CreateAndRegisterStream method
        // Using in Render
        public System.IO.Stream IntermediateCreateAndRegisterStream(string name, string extension, System.Text.Encoding encoding, string mimeType
            , bool willSeek, Microsoft.ReportingServices.Interfaces.StreamOper operation)
        {
            _name = name;
            _encoding = encoding;
            _extension = extension;
            _mimeType = mimeType;
            _operation = operation;
            _willSeek = willSeek;
            intermediateStream = new System.IO.MemoryStream();
            return intermediateStream;
        }

        bool Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.Render(Microsoft.ReportingServices.OnDemandReportRendering.Report report
            , System.Collections.Specialized.NameValueCollection reportServerParameters, System.Collections.Specialized.NameValueCollection deviceInfo
            , System.Collections.Specialized.NameValueCollection clientCapabilities, ref System.Collections.Hashtable renderProperties
            , Microsoft.ReportingServices.Interfaces.CreateAndRegisterStream createAndRegisterStream)
        {
            // throw new System.NotImplementedException();

            //Call the render method of the intermediate rendering extension
            //pdfRenderer.Render(report, reportServerParameters, deviceInfo, clientCapabilities, ref renderProperties, new Microsoft.ReportingServices.Interfaces.CreateAndRegisterStream(IntermediateCreateAndRegisterStream));

            //Register stream for new rendering extension
            System.IO.Stream outputStream = createAndRegisterStream(_name, _extension, _encoding, _mimeType, _willSeek, _operation);

            intermediateStream.Position = 0;

            //put stream update code here

            
            //Copy the stream to the outout stream
            byte[] buffer = new byte[32768];
            while (true)
            {
                int read = intermediateStream.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                    break;
                outputStream.Write(buffer, 0, read);
            }
            buffer = null;

            intermediateStream.Close();

            return false;
        }

        bool Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension.RenderStream(string streamName, Microsoft.ReportingServices.OnDemandReportRendering.Report report, System.Collections.Specialized.NameValueCollection reportServerParameters, System.Collections.Specialized.NameValueCollection deviceInfo, System.Collections.Specialized.NameValueCollection clientCapabilities, ref System.Collections.Hashtable renderProperties, Microsoft.ReportingServices.Interfaces.CreateAndRegisterStream createAndRegisterStream)
        {
            // throw new System.NotImplementedException();
            return false;
        }

        string Microsoft.ReportingServices.Interfaces.IExtension.LocalizedName
        {
            get { return "PDF Renderer with background"; }
        }

        void Microsoft.ReportingServices.Interfaces.IExtension.SetConfiguration(string configuration)
        {
            throw new System.NotImplementedException();
            // pdfRenderer.SetConfiguration(configuration);
        }
    }
}

#endif 
