using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace SimpleHttpServer
{
    /// <summary>
    ///监听的地址
    /// </summary>
    [XmlType("address")]
    public class ListenAddress
    {
        /// <summary>
        /// 地址名称
        /// </summary>
        [XmlAttribute("name")]
        public String Name { get; set; }

        [XmlAttribute("host")]
        public String Host { get; set; }

        [XmlAttribute("port")]
        public Int32 Port { get; set; }

        /// <summary>
        /// 是否使用 https  ，暂未支持
        /// </summary>
        [XmlAttribute("isHttps")]
        public Boolean IsHttps { get; set; }
    }
}
