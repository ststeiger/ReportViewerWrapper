using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Embedded2017
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für report
    /// </summary>
    public class report : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";

            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                foreach (string key in context.Request.Headers.AllKeys)
                {
                    if (string.Equals(key, "Connection", StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    if (string.Equals(key, "Host", StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    if (string.Equals(key, "Accept-Encoding", StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    string value = context.Request.Headers[key];
                    wc.Headers[key] = value;
                }

                // wc.Headers["Accept-Encoding"] = "identity";


                /* {Cache-Control: no-cache
                Connection: keep - alive
                Pragma: no - cache
                Accept: text / html,application / xhtml + xml,application / xml; q = 0.9,image / webp,image / apng,*;q=0.8
                Accept-Encoding: gzip, deflate, br
                Accept-Language: en-US,en;q=0.9,de-CH;q=0.8,de;q=0.7,fr-CH;q=0.6,fr;q=0.5
                Cookie: ai_user=WMz8a|2018-07-05T11:26:28.142Z; ASP.NET_SessionId=5owxpsvvivzefrdrqsekcpnj; ai_session=dZRze|1530867342103|1530867408379.6
                Host: localhost:61499
                User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36
                Upgrade-Insecure-Requests: 1
                DNT: 1
                */
                // string data = wc.DownloadString("http://localhost:61499/TestRender.aspx");
                // context.Response.Write(data);
                byte[] ba = wc.DownloadData("http://localhost:61499/TestRender.aspx");
                context.Response.BinaryWrite(ba);
                
            } // End Using wc 

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}