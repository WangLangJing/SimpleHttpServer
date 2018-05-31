using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace SimpleHttpServer
{
    /// <summary>
    /// Server 运行时的配置
    /// </summary>
    [XmlRoot]
    public class ServerConfig
    {
        /// <summary>
        /// 服务的根目录
        /// </summary>
        [XmlElement("RootDirectory")]
        public String RootDirectory { get; set; } /*= AppDomain.CurrentDomain.BaseDirectory;*/

        [XmlElement("Listen")]
        public ListenConfig ListenConfig { get; set; }

        [XmlElement("HttpHandlers")]
        public HandlerConfig HandlerConfig { get; set; }

        [XmlElement("Work")]
        public WorkConfig WorkConfig { get; set; }

        [XmlElement("StaticContent")]
        public StaticContentConfig StaticContentConfig { get; set; }
 #if NETCOREAPP20
        [XmlElement("Timeout")]
        public TimeoutConfig TimeoutConfig { get; set; }
#endif
    }
}
