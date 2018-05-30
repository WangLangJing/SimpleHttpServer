using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
namespace SimpleHttpServer
{
    /// <summary>
    /// 管理 Http Server 运行时的环境
    /// </summary>
    public static class HttpServerRuntime
    {
        internal static String ServerConfigPath;
        public static ServerConfig ServerConfig { get; private set; }

        static HttpServerRuntime()
        {
            ServerConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Server.config");
            Initialize();
        }

        private volatile static Boolean _initialized;
        public static Boolean Initialized { get => _initialized; }



        public static void Initialize()
        {
            if (!_initialized)
            {
                if (File.Exists(ServerConfigPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ServerConfig));
                    using (FileStream stream = new FileStream(ServerConfigPath, FileMode.Open, FileAccess.Read))
                    {
                        ServerConfig = serializer.Deserialize(stream) as ServerConfig;
                    }
                }
                if (ServerConfig == null)
                {
                    ServerConfig = new ServerConfig();
                }
                _initialized = true;
            }
        }
        public static void Release()
        {
            if (_initialized)
            {
                //...
            }
        }
    }
}
