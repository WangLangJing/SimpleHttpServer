using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace SimpleHttpServer
{
    public interface IHttpHandler
    {
        /// <summary>
        /// 是否可重复使用
        /// </summary>
        Boolean IsReusable { get; }
        void ProcessContext(HttpContext context);
    }
}
