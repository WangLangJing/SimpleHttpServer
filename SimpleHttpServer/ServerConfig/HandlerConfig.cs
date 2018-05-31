using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace SimpleHttpServer
{

    public class HandlerConfig
    {
        /// <summary>
        /// 处理对象通配
        /// </summary>
        [XmlArray("HandlerMatch")]
        public HandlerMatching[] Handlers { get; set; }
    }
}
