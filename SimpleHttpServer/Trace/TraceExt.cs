using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace SimpleHttpServer
{

    public static class TraceExt
    {
        [Conditional("TRACE")]
        public static void WriteLineWithTime(String info)
        {
           
            Trace.WriteLine(" "+info + "   " + DateTime.Now.ToString("HH:mm:ss fff"));
        }
    }
}
