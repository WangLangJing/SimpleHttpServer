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
        /// <summary>
        /// 服务的根目录
        /// </summary>
        public String RootDirectory { get; private set; }

        /// <summary>
        /// 上下文的处理对象
        /// </summary>
        public IHttpHandler Handler { get;internal set; }

        internal static HttpContext Wrap(HttpListenerContext original, HttpServer server)
        {
            var context = new HttpContext(original);
            context.RootDirectory = server.RootDirectory;
            return context;
        }

        internal HttpContext(HttpListenerContext original)
        {
            this.Entity = original;
        }

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
    }
}
