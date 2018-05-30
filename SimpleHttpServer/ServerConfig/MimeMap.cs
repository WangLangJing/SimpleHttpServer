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
        [XmlAttribute("ext")]
        public String FileExtension { get; set; }

        [XmlAttribute("mimeType")]
        public String MimeType { get; set; }
    }
}
