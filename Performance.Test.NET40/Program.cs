using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SimpleHttpServer;
namespace Performance.Test.NET40
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerConfig config = new ServerConfig();

            ListenConfig listenConfig = new ListenConfig();
            listenConfig.Addresses = new ListenAddress[1];
            listenConfig.Addresses[0] = new ListenAddress
            {
                Host = "127.0.0.1",
                Name = "1",
                Port = 8080
            };

            WorkConfig workConfig = new WorkConfig();

            workConfig.ProcessQueueMaxLength = 2000;
            workConfig.WorkThreadNum = 8;

            HandlerConfig handlerConfig = new HandlerConfig();

            handlerConfig.Handlers = new HandlerMatching[1];
            handlerConfig.Handlers[0] = new HandlerMatching
            {
                Select = "http://127.0.0.1:8080/*:",
                Type = "Test,Test.TestHandler"
            };

            StaticContentConfig staticContentConfig = new StaticContentConfig();
            staticContentConfig.MimeMaps = new MimeMap[1];
            staticContentConfig.MimeMaps[0] = new MimeMap
            {
                FileExtension = ".repx",
                MimeType = "test"
            };

            config.RootDirectory = @"d:\test\web";
            config.HandlerConfig = handlerConfig;
            config.WorkConfig = workConfig;
            config.ListenConfig = listenConfig;
            config.StaticContentConfig = staticContentConfig;

            XmlSerializer serializer = new XmlSerializer(typeof(ServerConfig));
            using (FileStream stream = new FileStream("Server.config", FileMode.CreateNew, FileAccess.Write))
            {
                serializer.Serialize(stream, config);
            }
        }
    }
}
