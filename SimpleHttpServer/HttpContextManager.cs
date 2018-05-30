using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
namespace SimpleHttpServer
{
    public class HttpContextManager
    {
        private HandlerManager _handlerManager;
        private ServerConfig _serverConfig;

        public HttpContextManager()
        {
            _handlerManager = new HandlerManager();
            _serverConfig = HttpServerRuntime.ServerConfig;
        }

        public void Forward(HttpContext context)
        {
            //选择请求处理对象
            var handler = _handlerManager.SelectHandler(context);
            context.Handler = handler;
            this.Dispatch(context);
        }

        private void Work(Object state)
        {
            var context = state as HttpContext;
            TraceExt.WriteLineWithTime("ready work context");
            context.Handler.ProcessContext(context);
            TraceExt.WriteLineWithTime("processed context");
        }
        public void Dispatch(HttpContext context)
        {
            TraceExt.WriteLineWithTime("dispath context");
            ThreadPool.QueueUserWorkItem(Work, context);
        }
    }
}
