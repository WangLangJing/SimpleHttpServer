using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace SimpleHttpServer
{
    [XmlType("MimeMap")]
    public class MimeMap
    {
        /// <summary>
        /// 文件后缀名
        /// </summary>
        [XmlAttribute("ext")]
        public String FileExtension { get; set; }

        /// <summary>
        /// 映射的 Mime 类型
        /// </summary>
        [XmlAttribute("mimeType")]
        public String MimeType { get; set; }
    }
}
