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
        [XmlElement]
        public String RootDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        [XmlElement("Listen")]
        public ListenConfig ListenConfig { get; set; } = new ListenConfig();

        [XmlElement("HttpHandlers")]
        public HandlerConfig HandlerConfig { get; set; } = new HandlerConfig();


        [XmlElement("StaticContent")]
        public StaticContentConfig StaticContentConfig { get; set; } = new StaticContentConfig();
    }
}
