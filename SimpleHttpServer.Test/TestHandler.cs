using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
