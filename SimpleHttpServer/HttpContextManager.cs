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
    public class HttpContextManager : IDisposable
    {
        /// <summary>
        /// 当前处理队列中请求的数量
        /// </summary>
        public volatile Int32 currentQueueNum = 0;

        private HandlerManager _handlerManager;
        private WorkConfig _workConfig;

        private Thread[] _workThreads;

        private volatile Int32 _singal = 0;
        /// <summary>
        /// 请求处理的队列
        /// </summary>
        private ProcessQueue _processQueue;
        private HttpServerUtility _serverUtility;

        public HttpContextManager(WorkConfig workConfig, HandlerManager manager, HttpServerUtility serverUtility)
        {
            _handlerManager = manager;
            _workConfig = workConfig;
            _serverUtility = serverUtility;

            currentQueueNum = 0;
            _processQueue = new ProcessQueue();
            this.StartWorkThread();

        }
        private void StartWorkThread()
        {
            _workThreads = new Thread[_workConfig.WorkThreadNum];

            for (Int32 i = 0; i < _workConfig.WorkThreadNum; ++i)
            {
                _workThreads[i] = new Thread(ProcessThreadWork);
                _workThreads[i].Start();
            }
        }

        public void ProcessThreadWork()
        {
            while (_singal == 0)
            {
                //若果当前处理队列中有请求
                if (currentQueueNum > 0)
                {
                    var context = this._processQueue.Dequeue();
                    if (context != null)
                    {
                        Interlocked.Decrement(ref currentQueueNum);
                        var handler = _handlerManager.SelectHandler(context);
                        context.Handler = handler;
                        if (handler.IsAsync)
                        {
                            Task processTask = new Task(Process, context);
                            processTask.Start();
                        }
                        else
                        {
                            handler.ProcessContext(context);
                        }
                    }
                }
                //若无工作线程休眠
                else
                {
                    Thread.Sleep(50);
                }
            }
        }


        public void Forward(HttpContext context)
        {
            _processQueue.Enqueue(context);
            Interlocked.Increment(ref currentQueueNum);
        }

        private void Process(Object state)
        {
            var context = state as HttpContext;
            this.Process(context);
        }

        private void Process(HttpContext context)
        {
            try
            {
                context.Handler.ProcessContext(context);
            }
            catch
            {
            }
        }
        public void Dispose()
        {
            Interlocked.Increment(ref _singal);
            for (Int32 i = 0; i < _workThreads.Length; ++i)
            {
                _workThreads[i] = null;
            }
        }
    }
}
