using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
namespace SimpleHttpServer
{
    public class HttpContext
    {

        public HttpListenerContext Entity { get; private set; }

        public HttpListenerRequest Request
        {
            get
            {
                return this.Entity.Request;
            }
        }

        public HttpListenerResponse Response
        {
            get
            {
                return this.Entity.Response;
            }
        }

        /// <summary>
        /// 请求上下文的处理对象
        /// </summary>
        public IHttpHandler Handler { get; internal set; }

        public HttpServerUtility Server { get; internal set; }


        internal static HttpContext Wrap(HttpListenerContext original, ServerConfig config, HttpServerUtility utility)
        {
            var context = new HttpContext(original);
            context.Server = utility;
            return context;
        }

        internal HttpContext(HttpListenerContext original)
        {
            this.Entity = original;
        }

      
    }
}
