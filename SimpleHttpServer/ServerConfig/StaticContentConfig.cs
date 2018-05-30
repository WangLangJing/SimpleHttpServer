using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace SimpleHttpServer
{

    public class StaticContentConfig
    {

        [XmlArray("MimeMaps")]
        public MimeMap[] MimeMaps { get; set; }
    }
}
