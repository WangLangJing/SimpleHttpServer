using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SimpleHttpServer.Test
{
    public class TestHandler : IHttpHandler
    {
        public Boolean IsReusable => true;

        public Boolean IsAsync => false;

        public void ProcessContext(HttpContext context)
        {
            Console.WriteLine("Obj Id:"+this.GetHashCode());
            Console.WriteLine(context.Request.Url.OriginalString);
            context.Response.Redirect("http://www.baidu.com");
            //String filename = context.Request.Url.AbsolutePath;
            //if (!String.IsNullOrEmpty(filename))
            //{
            //    filename = filename.Substring(1);
            //}
            //filename = context.Server.MapPhysicsPath(filename);
            //HttpStatusCode statusCode = HttpStatusCode.OK;
            //if (File.Exists(filename))
            //{
            //    try
            //    {
            //        Stream remoteStream = context.Request.InputStream;
            //        using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            //        {
            //            context.Response.ContentType = context.Server.MimeTypes[Path.GetExtension(filename)];
            //            context.Response.ContentLength64 = stream.Length;
            //            stream.CopyTo(context.Response.OutputStream);
            //            stream.Flush();
            //            context.Response.OutputStream.Flush();
            //        }
            //    }
            //    catch
            //    {
            //        statusCode = HttpStatusCode.InternalServerError;
            //    }
            //}
            //else
            //{
            //    statusCode = HttpStatusCode.NotFound;
            //}
            //context.Response.StatusCode = (Int32)statusCode;
            //if (statusCode == HttpStatusCode.OK)
            //{
            //    context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
            //    context.Response.AddHeader("Last-Modified", File.GetLastWriteTime(filename).ToString("r"));
            //}
            //context.Response.OutputStream.Close();
        }
    }
}
