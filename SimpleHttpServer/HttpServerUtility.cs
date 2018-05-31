using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace SimpleHttpServer
{
    /// <summary>
    /// 用于处理 Web 请求的帮助方法的集合
    /// </summary>
    public class HttpServerUtility
    {
        private ServerConfig _serverConfig;
        /// <summary>
        /// Mime 类型映射
        /// </summary>
        public MimeTypes MimeTypes { get; internal set; }

        /// <summary>
        /// 映射指定文件在服务器上的物理路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public String MapPhysicsPath(String filename)
        {
            String mapPath = null;
            mapPath = Path.Combine(_serverConfig.RootDirectory, filename);
            return mapPath;
        }
        public HttpServerUtility(ServerConfig serverConfig)
        {
            _serverConfig = serverConfig;
            this.MimeTypes = new MimeTypes(serverConfig.StaticContentConfig);
        }
    }
}
