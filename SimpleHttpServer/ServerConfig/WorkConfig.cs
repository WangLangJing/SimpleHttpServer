using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
namespace SimpleHttpServer
{

    public class WorkConfig
    {
        /// <summary>
        /// 处理队列的最大长度
        /// </summary>
        [XmlElement("ProcessQueueMaxLength")]
        public Int32 ProcessQueueMaxLength { get; set; }
        /// <summary>
        /// 处理请求的工作线程的数目
        /// </summary>
        [XmlElement("WorkThreadNum")]
        public Int32 WorkThreadNum { get; set; }
    }
}
