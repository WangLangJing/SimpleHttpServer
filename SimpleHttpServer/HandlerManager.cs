using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace SimpleHttpServer
{
    public class HandlerManager 
    {
        public IHttpHandler SelectHandler(HttpContext context)
        {
            IHttpHandler handler = new DefaultHandler();
            return handler;
        }

    }
}
