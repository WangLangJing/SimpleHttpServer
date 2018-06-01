#if NETCOREAPP2_0
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
namespace SimpleHttpServer
{

    public class TimeoutConfig
    {
        /// <summary>
        ///允许 HttpListener 在保持连接时侦听完实体正文的时间。
        /// </summary>
        [XmlAttribute("drainEntityBody")]
        public TimeSpan? DrainEntityBody { get; set; }

        /// <summary>
        ///允许请求实体正文到达的时间（以秒为单位）。
        /// </summary>
        [XmlAttribute("entityBody")]
        public TimeSpan? EntityBody { get; set; }

        /// <summary>
        ///允许 HttpListener 分析请求标头的时间（以秒为单位）。
        /// </summary>
        [XmlAttribute("headerWait")]
        public TimeSpan? HeaderWait { get; set; }


        /// <summary>
        /// 获取或设置允许空闲连接的时间（以秒为单位）。
        /// </summary>
        [XmlAttribute("idleConnection")]
        public TimeSpan? IdleConnection { get; set; }


        /// <summary>
        ///获取或设置响应的最低发送速率（以每秒字节数为单位）。
        /// </summary>
        [XmlAttribute("minSendBytesPerSecond")]
        public Int64? MinSendBytesPerSecond { get; set; }

        /// <summary>
        ///在 HttpListener 选取请求前允许请求在请求队列中停留的时间（以秒为单位）。
        /// </summary>
        [XmlAttribute("requestQueue")]
        public TimeSpan? RequestQueue { get; set; }
    }

}
#endif
