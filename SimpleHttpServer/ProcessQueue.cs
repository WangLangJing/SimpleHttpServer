using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
namespace SimpleHttpServer
{
    /// <summary>
    /// 放置待处理请求的队列
    /// </summary>
    public class ProcessQueue
    {
        private ConcurrentQueue<HttpContext> _queue;

        public ProcessQueue()
        {
            _queue = new ConcurrentQueue<HttpContext>();
        }
        public void Enqueue(HttpContext context)
        {
            _queue.Enqueue(context);
        }

        /// <summary>
        /// 从处理队列中获取 待处理的上下文信息，若获取失败则返回 null
        /// </summary>
        /// <returns></returns>
        public HttpContext Dequeue()
        {
            HttpContext context = null;
            _queue.TryDequeue(out context);
            return context;
        }
    }
}
