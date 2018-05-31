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
        /// <summary>
        /// 是否异步处理
        /// </summary>
        Boolean IsAsync { get; }

        void ProcessContext(HttpContext context);
    }
}
