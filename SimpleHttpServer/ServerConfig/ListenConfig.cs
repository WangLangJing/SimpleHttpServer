using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace SimpleHttpServer
{
  
    public class ListenConfig
    {

        /// <summary>
        /// 监听地址的集合
        /// </summary>
        [XmlArray("Addresses")]
        public ListenAddress[] Addresses { get; set; }
    }
}
