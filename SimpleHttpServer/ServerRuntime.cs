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
        public static readonly String ServerConfigName = "Server.config";

        static HttpServerRuntime()
        {

        }


        public static ServerConfig LoadServerConfig(String directory)
        {
            ServerConfig config = null;
            String path = Path.Combine(directory, ServerConfigName);
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServerConfig));
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    config = serializer.Deserialize(stream) as ServerConfig;
                }
                CheckConfig(config);
            }
            else
            {
                config = DefaultConfig();
            }
            config.RootDirectory = directory;
            return config;
        }
        private static void CheckConfig(ServerConfig config)
        {

        }
        private static ServerConfig DefaultConfig()
        {
            ServerConfig config = new ServerConfig();

            config.WorkConfig = new WorkConfig();
            config.WorkConfig.ProcessQueueMaxLength = 2000;

            if (Environment.Is64BitOperatingSystem)
            {
                config.WorkConfig.WorkThreadNum = Environment.ProcessorCount / 2;
            }
            else
            {
                config.WorkConfig.WorkThreadNum = Environment.ProcessorCount;
            }

            config.ListenConfig = new ListenConfig();

            config.StaticContentConfig = new StaticContentConfig();

            config.HandlerConfig = new HandlerConfig();


            return config;
        }
        public static void Release()
        {

        }
    }
}
