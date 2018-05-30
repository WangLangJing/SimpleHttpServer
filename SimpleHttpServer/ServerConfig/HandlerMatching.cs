using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace SimpleHttpServer
{
    [XmlType("Match")]
    public class HandlerMatching
    {
        /// <summary>
        /// 可与请求 URL 匹配的包含通配符的字符串
        /// </summary>
        [XmlAttribute("select")]
        public String Select { get; set; }

        [XmlAttribute("type")]
        public String Type { get; set; }
    }
}
