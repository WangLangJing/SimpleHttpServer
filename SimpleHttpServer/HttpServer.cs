using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;


namespace SimpleHttpServer
{
    public enum HttpServerStatus : Int32
    {
        /// <summary>
        /// 准备就绪，这是最初的状态
        /// </summary>
        Ready = 0,
        /// <summary>
        /// 监听中
        /// </summary>
        Listening=1,
        /// <summary>
        /// 已停止
        /// </summary>
        Stopped=2,
        /// <summary>
        /// 已关闭
        /// </summary>
        Closed=3,
        /// <summary>
        /// 混乱，处于此状态的 Server 不可用
        /// </summary>
        Chaos=4
    }
    public class HttpServer
    {


        /// <summary>
        /// 服务器当前的状态
        /// </summary>
        public HttpServerStatus Status
        {
            get
            {
                return (HttpServerStatus)_status;
            }
            private set
            {
                Interlocked.Exchange(ref this._status, (Int32)value);
            }
        }

        private volatile Int32 _status = (Int32)HttpServerStatus.Ready;

        /// <summary>
        ///服务器的根目录
        /// </summary>
        public String RootDirectory { get; private set; }
        private ServerConfig _serverConfig;

        const String HttpUrlFormat = "http://{0}:{1}/";
        const String HttpsUrlForamt = "https://{0}:{1}/";


        private HttpListener _httpListener;
        private HttpContextManager _httpContentManager;
        private HandlerManager _handlerManager;
        private HttpServerUtility _httpServerUtility;

        private Thread _listenThread; //请求监听的线程

        public HttpServer(String configPath = null)
        {
            _httpListener = new HttpListener();
            this.Initialize(configPath);
            _httpServerUtility = new HttpServerUtility(_serverConfig);

            _handlerManager = new HandlerManager(_serverConfig.HandlerConfig, _httpServerUtility);
            _httpContentManager = new HttpContextManager(_serverConfig.WorkConfig, _handlerManager, _httpServerUtility);


            _listenThread = new Thread(HttpListen);
            _listenThread.Start();
        }

        public void Initialize(String configPath)
        {
            this._serverConfig = HttpServerRuntime.LoadServerConfig(configPath);
            this.EnableConfig(this._serverConfig);
            if (!Directory.Exists(this.RootDirectory))
            {
                throw new DirectoryNotFoundException("root directory not found");
            }
        }
        private void EnableConfig(ServerConfig config)
        {
#if NETCOREAPP20
            if (config.TimeoutConfig != null)
            {
                var tc = config.TimeoutConfig;

                if (tc.DrainEntityBody.HasValue)
                    _httpListener.TimeoutManager.DrainEntityBody = tc.DrainEntityBody.Value;

                if (tc.EntityBody.HasValue)
                    _httpListener.TimeoutManager.EntityBody = tc.EntityBody.Value;

                if (tc.HeaderWait.HasValue)
                    _httpListener.TimeoutManager.HeaderWait = tc.HeaderWait.Value;

                if (tc.IdleConnection.HasValue)
                    _httpListener.TimeoutManager.IdleConnection = tc.IdleConnection.Value;

                if (tc.MinSendBytesPerSecond.HasValue)
                    _httpListener.TimeoutManager.MinSendBytesPerSecond = tc.MinSendBytesPerSecond.Value;

                if (tc.RequestQueue.HasValue)
                    _httpListener.TimeoutManager.RequestQueue = tc.RequestQueue.Value;
            }
#endif
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        public void Start(IEnumerable<ListenAddress> addresses = null)
        {
            List<ListenAddress> list = new List<ListenAddress>();
            if (addresses != null)
            {
                list.AddRange(addresses);

            }
            if (_serverConfig.ListenConfig.Addresses != null)
            {
                list.AddRange(_serverConfig.ListenConfig.Addresses);
            }
            foreach (ListenAddress addr in list)
            {
                String format = HttpUrlFormat;
                if (addr.IsHttps)
                {
                    format = HttpsUrlForamt;
                }
                _httpListener.Prefixes.Add(String.Format(format, addr.Host, addr.Port.ToString()));
            }
            if (_httpListener.Prefixes.Count <= 0)
            {
                throw new InvalidOperationException("not found listen address!");
            }


            this._httpListener.Start();
            this.Status = HttpServerStatus.Listening;

        }
        /// <summary>
        /// 停止监听并释放相应资源
        /// </summary>
        public void Close()
        {
            this.Status = HttpServerStatus.Closed;
            this._httpListener.Close();
            this._httpListener = null;
            this._listenThread = null;
            this._httpContentManager.Dispose();
        }
        /// <summary>
        /// 停止监听
        /// </summary>
        public void Stop()
        {

            this.Status = HttpServerStatus.Stopped;
            this._httpListener.Stop();
        }



        private void HttpListen()
        {
            Int32 count = 0;
            while (true)
            {
                switch ((HttpServerStatus)this._status)
                {

                    case HttpServerStatus.Listening:
                        {
                        
                            HttpListenerContext oriContext = null;
                            try
                            {
                                oriContext = this._httpListener.GetContext();
                                TraceExt.WriteLineWithTime("context count "+ (++count).ToString());
                            }
                            catch
                            {
                                if (this._status == (Int32)HttpServerStatus.Listening)
                                {
                                    throw;
                                }
                            }
                            if (_httpContentManager.currentQueueNum > this._serverConfig.WorkConfig.ProcessQueueMaxLength)
                            {
                                oriContext.Response.Abort();
                            }
                
                            TraceExt.WriteLineWithTime("received request");

                            var httpContext = HttpContext.Wrap(oriContext, this._serverConfig, _httpServerUtility);

                            TraceExt.WriteLineWithTime("wraped context");

                            this._httpContentManager.Forward(httpContext);
                        }
                        break;
                    case HttpServerStatus.Ready:
                    case HttpServerStatus.Stopped:
                        {
                            TraceExt.WriteLineWithTime("server sleep");
                            Thread.Sleep(50);
                        }
                        break;
                    case HttpServerStatus.Closed:
                    case HttpServerStatus.Chaos:
                        {
                            TraceExt.WriteLineWithTime("server closed");
                        }
                        return;
                }

            }
        }
    }
}
