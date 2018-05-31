using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleHttpServer
{
    public class MimeTypes
    {
        public String this[String ext]
        {
            get
            {
                return this.GetMimeType(ext);
            }
        }

        public Dictionary<String, String> _mapTable;

        public MimeTypes(StaticContentConfig config)
        {
            var maps = config.MimeMaps;
            _mapTable = CreateMapTable();
            if (maps != null && maps.Length >= 0)
            {
                foreach (var map in maps)
                {
                    SetMimeType(map.FileExtension, map.MimeType);
                }
            }
        }
        private Dictionary<String, String> CreateMapTable()
        {
            var mapTable = new Dictionary<String, String>();
            mapTable.Add(".323", "text/h323");
            mapTable.Add(".asx", "video/x-ms-asf");
            mapTable.Add(".acx", "application/internet-property-stream");
            mapTable.Add(".ai", "application/postscript");
            mapTable.Add(".aif", "audio/x-aiff");
            mapTable.Add(".aiff", "audio/aiff");
            mapTable.Add(".axs", "application/olescript");
            mapTable.Add(".aifc", "audio/aiff");
            mapTable.Add(".asr", "video/x-ms-asf");
            mapTable.Add(".avi", "video/x-msvideo");
            mapTable.Add(".asf", "video/x-ms-asf");
            mapTable.Add(".au", "audio/basic");
            mapTable.Add(".application", "application/x-ms-application");
            mapTable.Add(".bin", "application/octet-stream");
            mapTable.Add(".bas", "text/plain");
            mapTable.Add(".bcpio", "application/x-bcpio");
            mapTable.Add(".bmp", "image/bmp");
            mapTable.Add(".cdf", "application/x-cdf");
            mapTable.Add(".cat", "application/vndms-pkiseccat");
            mapTable.Add(".crt", "application/x-x509-ca-cert");
            mapTable.Add(".c", "text/plain");
            mapTable.Add(".css", "text/css");
            mapTable.Add(".cer", "application/x-x509-ca-cert");
            mapTable.Add(".crl", "application/pkix-crl");
            mapTable.Add(".cmx", "image/x-cmx");
            mapTable.Add(".csh", "application/x-csh");
            mapTable.Add(".cod", "image/cis-cod");
            mapTable.Add(".cpio", "application/x-cpio");
            mapTable.Add(".clp", "application/x-msclip");
            mapTable.Add(".crd", "application/x-mscardfile");
            mapTable.Add(".deploy", "application/octet-stream");
            mapTable.Add(".dll", "application/x-msdownload");
            mapTable.Add(".dot", "application/msword");
            mapTable.Add(".doc", "application/msword");
            mapTable.Add(".dvi", "application/x-dvi");
            mapTable.Add(".dir", "application/x-director");
            mapTable.Add(".dxr", "application/x-director");
            mapTable.Add(".der", "application/x-x509-ca-cert");
            mapTable.Add(".dib", "image/bmp");
            mapTable.Add(".dcr", "application/x-director");
            mapTable.Add(".disco", "text/xml");
            mapTable.Add(".exe", "application/octet-stream");
            mapTable.Add(".etx", "text/x-setext");
            mapTable.Add(".evy", "application/envoy");
            mapTable.Add(".eml", "message/rfc822");
            mapTable.Add(".eps", "application/postscript");
            mapTable.Add(".flr", "x-world/x-vrml");
            mapTable.Add(".fif", "application/fractals");
            mapTable.Add(".gtar", "application/x-gtar");
            mapTable.Add(".gif", "image/gif");
            mapTable.Add(".gz", "application/x-gzip");
            mapTable.Add(".hta", "application/hta");
            mapTable.Add(".htc", "text/x-component");
            mapTable.Add(".htt", "text/webviewhtml");
            mapTable.Add(".h", "text/plain");
            mapTable.Add(".hdf", "application/x-hdf");
            mapTable.Add(".hlp", "application/winhlp");
            mapTable.Add(".html", "text/html");
            mapTable.Add(".htm", "text/html");
            mapTable.Add(".hqx", "application/mac-binhex40");
            mapTable.Add(".isp", "application/x-internet-signup");
            mapTable.Add(".iii", "application/x-iphone");
            mapTable.Add(".ief", "image/ief");
            mapTable.Add(".ivf", "video/x-ivf");
            mapTable.Add(".ins", "application/x-internet-signup");
            mapTable.Add(".ico", "image/x-icon");
            mapTable.Add(".jpg", "image/jpeg");
            mapTable.Add(".jfif", "image/pjpeg");
            mapTable.Add(".jpe", "image/jpeg");
            mapTable.Add(".jpeg", "image/jpeg");
            mapTable.Add(".js", "application/x-javascript");
            mapTable.Add(".lsx", "video/x-la-asf");
            mapTable.Add(".latex", "application/x-latex");
            mapTable.Add(".lsf", "video/x-la-asf");
            mapTable.Add(".manifest", "application/x-ms-manifest");
            mapTable.Add(".mhtml", "message/rfc822");
            mapTable.Add(".mny", "application/x-msmoney");
            mapTable.Add(".mht", "message/rfc822");
            mapTable.Add(".mid", "audio/mid");
            mapTable.Add(".mpv2", "video/mpeg");
            mapTable.Add(".man", "application/x-troff-man");
            mapTable.Add(".mvb", "application/x-msmediaview");
            mapTable.Add(".mpeg", "video/mpeg");
            mapTable.Add(".m3u", "audio/x-mpegurl");
            mapTable.Add(".mdb", "application/x-msaccess");
            mapTable.Add(".mpp", "application/vnd.ms-project");
            mapTable.Add(".m1v", "video/mpeg");
            mapTable.Add(".mpa", "video/mpeg");
            mapTable.Add(".me", "application/x-troff-me");
            mapTable.Add(".m13", "application/x-msmediaview");
            mapTable.Add(".movie", "video/x-sgi-movie");
            mapTable.Add(".m14", "application/x-msmediaview");
            mapTable.Add(".mpe", "video/mpeg");
            mapTable.Add(".mp2", "video/mpeg");
            mapTable.Add(".mov", "video/quicktime");
            mapTable.Add(".mp3", "audio/mpeg");
            mapTable.Add(".mpg", "video/mpeg");
            mapTable.Add(".ms", "application/x-troff-ms");
            mapTable.Add(".nc", "application/x-netcdf");
            mapTable.Add(".nws", "message/rfc822");
            mapTable.Add(".oda", "application/oda");
            mapTable.Add(".ods", "application/oleobject");
            mapTable.Add(".pmc", "application/x-perfmon");
            mapTable.Add(".p7r", "application/x-pkcs7-certreqresp");
            mapTable.Add(".p7b", "application/x-pkcs7-certificates");
            mapTable.Add(".p7s", "application/pkcs7-signature");
            mapTable.Add(".pmw", "application/x-perfmon");
            mapTable.Add(".ps", "application/postscript");
            mapTable.Add(".p7c", "application/pkcs7-mime");
            mapTable.Add(".pbm", "image/x-portable-bitmap");
            mapTable.Add(".ppm", "image/x-portable-pixmap");
            mapTable.Add(".pub", "application/x-mspublisher");
            mapTable.Add(".pnm", "image/x-portable-anymap");
            mapTable.Add(".png", "image/png");
            mapTable.Add(".pml", "application/x-perfmon");
            mapTable.Add(".p10", "application/pkcs10");
            mapTable.Add(".pfx", "application/x-pkcs12");
            mapTable.Add(".p12", "application/x-pkcs12");
            mapTable.Add(".pdf", "application/pdf");
            mapTable.Add(".pps", "application/vnd.ms-powerpoint");
            mapTable.Add(".p7m", "application/pkcs7-mime");
            mapTable.Add(".pko", "application/vndms-pkipko");
            mapTable.Add(".ppt", "application/vnd.ms-powerpoint");
            mapTable.Add(".pmr", "application/x-perfmon");
            mapTable.Add(".pma", "application/x-perfmon");
            mapTable.Add(".pot", "application/vnd.ms-powerpoint");
            mapTable.Add(".prf", "application/pics-rules");
            mapTable.Add(".pgm", "image/x-portable-graymap");
            mapTable.Add(".qt", "video/quicktime");
            mapTable.Add(".ra", "audio/x-pn-realaudio");
            mapTable.Add(".rgb", "image/x-rgb");
            mapTable.Add(".ram", "audio/x-pn-realaudio");
            mapTable.Add(".rmi", "audio/mid");
            mapTable.Add(".ras", "image/x-cmu-raster");
            mapTable.Add(".roff", "application/x-troff");
            mapTable.Add(".rtf", "application/rtf");
            mapTable.Add(".rtx", "text/richtext");
            mapTable.Add(".sv4crc", "application/x-sv4crc");
            mapTable.Add(".spc", "application/x-pkcs7-certificates");
            mapTable.Add(".setreg", "application/set-registration-initiation");
            mapTable.Add(".snd", "audio/basic");
            mapTable.Add(".stl", "application/vndms-pkistl");
            mapTable.Add(".setpay", "application/set-payment-initiation");
            mapTable.Add(".stm", "text/html");
            mapTable.Add(".shar", "application/x-shar");
            mapTable.Add(".sh", "application/x-sh");
            mapTable.Add(".sit", "application/x-stuffit");
            mapTable.Add(".spl", "application/futuresplash");
            mapTable.Add(".sct", "text/scriptlet");
            mapTable.Add(".scd", "application/x-msschedule");
            mapTable.Add(".sst", "application/vndms-pkicertstore");
            mapTable.Add(".src", "application/x-wais-source");
            mapTable.Add(".sv4cpio", "application/x-sv4cpio");
            mapTable.Add(".tex", "application/x-tex");
            mapTable.Add(".tgz", "application/x-compressed");
            mapTable.Add(".t", "application/x-troff");
            mapTable.Add(".tar", "application/x-tar");
            mapTable.Add(".tr", "application/x-troff");
            mapTable.Add(".tif", "image/tiff");
            mapTable.Add(".txt", "text/plain");
            mapTable.Add(".texinfo", "application/x-texinfo");
            mapTable.Add(".trm", "application/x-msterminal");
            mapTable.Add(".tiff", "image/tiff");
            mapTable.Add(".tcl", "application/x-tcl");
            mapTable.Add(".texi", "application/x-texinfo");
            mapTable.Add(".tsv", "text/tab-separated-values");
            mapTable.Add(".ustar", "application/x-ustar");
            mapTable.Add(".uls", "text/iuls");
            mapTable.Add(".vcf", "text/x-vcard");
            mapTable.Add(".wps", "application/vnd.ms-works");
            mapTable.Add(".wav", "audio/wav");
            mapTable.Add(".wrz", "x-world/x-vrml");
            mapTable.Add(".wri", "application/x-mswrite");
            mapTable.Add(".wks", "application/vnd.ms-works");
            mapTable.Add(".wmf", "application/x-msmetafile");
            mapTable.Add(".wcm", "application/vnd.ms-works");
            mapTable.Add(".wrl", "x-world/x-vrml");
            mapTable.Add(".wdb", "application/vnd.ms-works");
            mapTable.Add(".wsdl", "text/xml");
            mapTable.Add(".xap", "application/x-silverlight-app");
            mapTable.Add(".xml", "text/xml");
            mapTable.Add(".xlm", "application/vnd.ms-excel");
            mapTable.Add(".xaf", "x-world/x-vrml");
            mapTable.Add(".xla", "application/vnd.ms-excel");
            mapTable.Add(".xls", "application/vnd.ms-excel");
            mapTable.Add(".xof", "x-world/x-vrml");
            mapTable.Add(".xlt", "application/vnd.ms-excel");
            mapTable.Add(".xlc", "application/vnd.ms-excel");
            mapTable.Add(".xsl", "text/xml");
            mapTable.Add(".xbm", "image/x-xbitmap");
            mapTable.Add(".xlw", "application/vnd.ms-excel");
            mapTable.Add(".xpm", "image/x-xpixmap");
            mapTable.Add(".xwd", "image/x-xwindowdump");
            mapTable.Add(".xsd", "text/xml");
            mapTable.Add(".z", "application/x-compress");
            mapTable.Add(".zip", "application/x-zip-compressed");
            return mapTable;
        }

        public void SetMimeType(String ext, String mimeType)
        {
            if (!String.IsNullOrEmpty(mimeType) && !String.IsNullOrEmpty(mimeType))
            {
                _mapTable[ext] = mimeType;
            }
        }
        /// <summary>
        /// 获取指定后缀名对应的MIME类型
        /// </summary>
        public String GetMimeType(String ext)
        {
            String contentType = "application/octet-stream";
            if (!String.IsNullOrEmpty(ext) && _mapTable.ContainsKey(ext))
            {
                contentType = _mapTable[ext];
            }
            return contentType;
        }

    }
}