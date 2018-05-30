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
        Listening,
        /// <summary>
        /// 已停止
        /// </summary>
        Stopped,
        /// <summary>
        /// 已关闭
        /// </summary>
        Closed,
        /// <summary>
        /// 混乱，处于此状态的 Server 不可用
        /// </summary>
        Chaos
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
                return _status;
            }
            private set
            {
                this._status = value;
            }
        }

        private volatile HttpServerStatus _status = HttpServerStatus.Ready;

        /// <summary>
        ///服务器的根目录
        /// </summary>
        public String RootDirectory { get; private set; }
        private ServerConfig _serverConfig;

        const String HttpUrlFormat = "http://{0}:{1}/";
        const String HttpsUrlForamt = "https://{0}:{1}/";


        private HttpListener _httpListener;
        private HttpContextManager _httpContentManager;

        private Thread _listenThread; //请求监听的线程

        public HttpServer()
        {
            _httpListener = new HttpListener();
            this.Initialize();
            _listenThread = new Thread(HttpListen);
            _httpContentManager = new HttpContextManager();
            _listenThread.Start();
        }

        public void Initialize()
        {
            _serverConfig = HttpServerRuntime.ServerConfig;
            this.RootDirectory = this._serverConfig.RootDirectory;
        }
        public void Setting(String rootDirectory, IEnumerable<ListenAddress> addresses)
        {
            if (String.IsNullOrWhiteSpace(rootDirectory))
            {
                throw new ArgumentException(nameof(rootDirectory));
            }
            if (File.Exists(rootDirectory))
            {
                throw new DirectoryNotFoundException(rootDirectory);
            }

            this.RootDirectory = rootDirectory;



        }

        /// <summary>
        /// 开始监听
        /// </summary>
        public void Start(IEnumerable<ListenAddress> addresses = null)
        {
            if (addresses != null)
            {
                foreach (ListenAddress addr in addresses)
                {
                    String format = HttpUrlFormat;
                    if (addr.IsHttps)
                    {
                        format = HttpsUrlForamt;
                    }
                    _httpListener.Prefixes.Add(String.Format(format, addr.Host, addr.Port.ToString()));
                }
            }
            if (_httpListener.Prefixes.Count <= 0)
            {
                throw new InvalidOperationException("not found listen address!");
            }

            this.Status = HttpServerStatus.Listening;
            this._httpListener.Start();


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
        }
        /// <summary>
        /// 停止监听
        /// </summary>
        public void Stop()
        {

            this.Status = HttpServerStatus.Stopped;
            this._httpListener.Stop();
        }

        private void Forward(HttpContext context)
        {
            this._httpContentManager.Forward(context);
        }

        private void HttpListen()
        {
            while (true)
            {
                switch (this._status)
                {

                    case HttpServerStatus.Listening:
                        {
                          
                            var oriContext = this._httpListener.GetContext();
                            TraceExt.WriteLineWithTime("received request");
                            var httpContext = HttpContext.Wrap(oriContext, this);
                            TraceExt.WriteLineWithTime("wraped context");
                            this.Forward(httpContext);
                        }
                        break;
                    case HttpServerStatus.Ready:
                    case HttpServerStatus.Stopped:
                        {
                            Thread.Sleep(50);
                        }
                        break;
                    case HttpServerStatus.Closed:
                    case HttpServerStatus.Chaos:
                        {

                        }
                        return;
                }

            }
        }
    }
}
