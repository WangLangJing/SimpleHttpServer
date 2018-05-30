using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
namespace SimpleHttpServer
{
    public class DefaultHandler : IHttpHandler
    {
        public Boolean IsReusable => true;


        public void ProcessContext(HttpContext context)
        {
            String filename = context.Request.Url.AbsolutePath;
            if (!String.IsNullOrEmpty(filename))
            {
                filename = filename.Substring(1);
            }
            filename = Path.Combine(context.RootDirectory, filename);
            HttpStatusCode statusCode= HttpStatusCode.OK;
            if (File.Exists(filename))
            {
                try
                {
                    Stream remoteStream = context.Request.InputStream;
                    using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        context.Response.ContentType = MimeAssistor.GetMimeType(Path.GetExtension(filename));
                        context.Response.ContentLength64 = stream.Length;
                        stream.CopyTo(context.Response.OutputStream);
                        stream.Flush();
                        context.Response.OutputStream.Flush();
                    }
                }
                catch
                {
                    statusCode = HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                statusCode = HttpStatusCode.NotFound;
            }
            context.Response.StatusCode = (Int32)statusCode;
            if (statusCode == HttpStatusCode.OK)
            {
                context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                context.Response.AddHeader("Last-Modified", File.GetLastWriteTime(filename).ToString("r"));
            }
            context.Response.OutputStream.Close();
        }
    }
}
