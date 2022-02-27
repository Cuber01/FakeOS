using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using FakeOS.General;

namespace FakeOS.Tools;

public class MimeTypes
{
    private static List<string> knownTypes;

    private static Dictionary<string, (string, string)> mimeTypesAndIcons;

    [DllImport("urlmon.dll", CharSet = CharSet.Auto)]
    private static extern UInt32 FindMimeFromData(
        UInt32 pBC, [MarshalAs(UnmanagedType.LPStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
        UInt32 cbSize, [MarshalAs(UnmanagedType.LPStr)] string pwzMimeProposed, UInt32 dwMimeFlags,
        ref UInt32 ppwzMimeOut, UInt32 dwReserverd
    );

    public static (string, string) GetContentType(string fileName)
    {
        if (knownTypes == null || mimeTypesAndIcons == null)
            InitializeMimeTypeLists();

        string extension = System.IO.Path.GetExtension(fileName).Replace(".", "").ToLower();
        mimeTypesAndIcons!.TryGetValue(extension, out var contentType);
        
        if (string.IsNullOrEmpty(contentType.Item1) || knownTypes!.Contains(contentType.Item1))
        {
            string headerType = ScanFileForMimeType(fileName);
            if (headerType != "application/octet-stream" || string.IsNullOrEmpty(contentType.Item1))
            {
                contentType = (headerType, AwesomeIcons.FileO);  
            }
              
        }

        return (contentType.Item1, contentType.Item2);
    }

    private static string ScanFileForMimeType(string fileName)
    {
        try
        {
            byte[] buffer = new byte[256];
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                int readLength = Convert.ToInt32(Math.Min(256, fs.Length));
                fs.Read(buffer, 0, readLength);
            }

            UInt32 mimeType = default(UInt32);
            FindMimeFromData(0, null, buffer, 256, null, 0, ref mimeType, 0);
            IntPtr mimeTypePtr = new IntPtr(mimeType);
            string mime = Marshal.PtrToStringUni(mimeTypePtr);
            Marshal.FreeCoTaskMem(mimeTypePtr);
            if (string.IsNullOrEmpty(mime))
                mime = "application/octet-stream";
            return mime;
        }
        catch (Exception ex)
        {
            return "application/octet-stream";
        }
    }

    private static void InitializeMimeTypeLists()
    {
        knownTypes = new string[]
        {
            "text/plain",
            "text/html",
            "text/xml",
            "text/richtext",
            "text/scriptlet",
            "audio/x-aiff",
            "audio/basic",
            "audio/mid",
            "audio/wav",
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png",
            "image/x-png",
            "image/tiff",
            "image/bmp",
            "image/x-xbitmap",
            "image/x-jg",
            "image/x-emf",
            "image/x-wmf",
            "video/avi",
            "video/mpeg",
            "application/octet-stream",
            "application/postscript",
            "application/base64",
            "application/macbinhex40",
            "application/pdf",
            "application/xml",
            "application/atom+xml",
            "application/rss+xml",
            "application/(x-compressed",
            "application/(x-zip-compressed",
            "application/(x-gzip-compressed",
            "application/(java",
            "application/(x-msdownload"
        }.ToList();

        mimeTypesAndIcons = new Dictionary<string, (string, string)>
        {
            { "abc", ("text/vnd.abc", AwesomeIcons.FileO) },
            { "acgi", ("text/html", AwesomeIcons.FileCodeO) },
            { "afl", ("video/animaflex", AwesomeIcons.FileO) },
            { "ai", ("application/postscript", AwesomeIcons.FileCodeO) },
            { "aif", ("audio/aiff", AwesomeIcons.FileAudioO) },
            { "aifc", ("audio/aiff", AwesomeIcons.FileAudioO) },
            { "aiff", ("audio/aiff", AwesomeIcons.FileAudioO) },
            { "aim", ("application/x-aim", AwesomeIcons.FileO) },
            { "aip", ("text/x-audiosoft-intra", AwesomeIcons.FileO) },
            { "ani", ("application/x-navi-animation", AwesomeIcons.FileO) },
            { "aos", ("application/x-nokia-9000-communicator-add-on-software", AwesomeIcons.FileO) },
            { "aps", ("application/mime", AwesomeIcons.FileO) },
            { "arc", ("application/octet-stream", AwesomeIcons.FileO) },
            { "arj", ("application/arj", AwesomeIcons.FileO) },
            { "art", ("image/x-jg", AwesomeIcons.FileImageO) },
            { "asf", ("video/x-ms-asf", AwesomeIcons.FileVideoO) },
            { "asm", ("text/x-asm", AwesomeIcons.FileO) },
            { "asp", ("text/asp", AwesomeIcons.FileO) },
            { "asx", ("application/x-mplayer2", AwesomeIcons.FileVideoO) },
            { "au", ("audio/basic", AwesomeIcons.FileO) },
            { "avi", ("video/avi", AwesomeIcons.FileVideoO) },
            { "avs", ("video/avs-video", AwesomeIcons.FileVideoO) },
            { "bcpio", ("application/x-bcpio", AwesomeIcons.FileO) },
            { "bin", ("application/octet-stream", AwesomeIcons.FileO) },
            { "bm", ("image/bmp", AwesomeIcons.FileImageO) },
            { "bmp", ("image/bmp", AwesomeIcons.FileImageO) },
            { "boo", ("application/book", AwesomeIcons.Book) },
            { "book", ("application/book", AwesomeIcons.Book) },
            { "boz", ("application/x-bzip2", AwesomeIcons.FileO) },
            { "bsh", ("application/x-bsh", AwesomeIcons.FileO) },
            { "bz", ("application/x-bzip", AwesomeIcons.FileO) },
            { "bz2", ("application/x-bzip2", AwesomeIcons.FileO) },
            { "c", ("text/plain", AwesomeIcons.FileO) },
            { "c++", ("text/plain", AwesomeIcons.FileO) },
            { "cat", ("application/vnd.ms-pki.seccat", AwesomeIcons.FileO) },
            { "cc", ("text/plain", AwesomeIcons.FileO) },
            { "ccad", ("application/clariscad", AwesomeIcons.FileO) },
            { "cco", ("application/x-cocoa", AwesomeIcons.FileO) },
            { "cdf", ("application/cdf", AwesomeIcons.FileO) },
            { "cer", ("application/pkix-cert", AwesomeIcons.FileO) },
            { "cha", ("application/x-chat", AwesomeIcons.FileO) },
            { "chat", ("application/x-chat", AwesomeIcons.FileO) },
            { "class", ("application/java", AwesomeIcons.FileCodeO) },
            { "com", ("application/octet-stream", AwesomeIcons.FileO) },
            { "conf", ("text/plain", AwesomeIcons.FileO) },
            { "cpio", ("application/x-cpio", AwesomeIcons.FileO) },
            { "cpp", ("text/x-c", AwesomeIcons.FileO) },
            { "cpt", ("application/x-cpt", AwesomeIcons.FileO) },
            { "crl", ("application/pkcs-crl", AwesomeIcons.FileO) },
            { "css", ("text/css", AwesomeIcons.FileCodeO) },
            { "def", ("text/plain", AwesomeIcons.FileO) },
            { "der", ("application/x-x509-ca-cert", AwesomeIcons.FileO) },
            { "dif", ("video/x-dv", AwesomeIcons.FileVideoO) },
            { "dir", ("application/x-director", AwesomeIcons.FileO) },
            { "dl", ("video/dl", AwesomeIcons.FileO) },
            { "doc", ("application/msword", AwesomeIcons.FileWordO) },
            { "dot", ("application/msword", AwesomeIcons.FileWordO) },
            { "dp", ("application/commonground", AwesomeIcons.FileO) },
            { "drw", ("application/drafting", AwesomeIcons.FileO) },
            { "dump", ("application/octet-stream", AwesomeIcons.FileO) },
            { "dv", ("video/x-dv", AwesomeIcons.FileO) },
            { "dvi", ("application/x-dvi", AwesomeIcons.FileO) },
            { "dwf", ("drawing/x-dwf (old)", AwesomeIcons.FileO) },
            { "dwg", ("application/acad", AwesomeIcons.FileO) },
            { "dxf", ("application/dxf", AwesomeIcons.FileO) },
            { "eps", ("application/postscript", AwesomeIcons.FileCodeO) },
            { "es", ("application/x-esrehber", AwesomeIcons.FileO) },
            { "etx", ("text/x-setext", AwesomeIcons.FileO) },
            { "evy", ("application/envoy", AwesomeIcons.FileO) },
            { "exe", ("application/octet-stream", AwesomeIcons.FileO) },
            { "f", ("text/plain", AwesomeIcons.FileO) },
            { "f90", ("text/x-fortran", AwesomeIcons.FileCodeO) },
            { "fdf", ("application/vnd.fdf", AwesomeIcons.FileO) },
            { "fif", ("image/fif", AwesomeIcons.FileO) },
            { "fli", ("video/fli", AwesomeIcons.FileO) },
            { "flv", ("video/x-flv", AwesomeIcons.FileO) },
            { "for", ("text/x-fortran", AwesomeIcons.FileO) },
            { "fpx", ("image/vnd.fpx", AwesomeIcons.FileO) },
            { "g", ("text/plain", AwesomeIcons.FileO) },
            { "g3", ("image/g3fax", AwesomeIcons.FileO) },
            { "gif", ("image/gif", AwesomeIcons.FileO) },
            { "gl", ("video/gl", AwesomeIcons.FileO) },
            { "gsd", ("audio/x-gsm", AwesomeIcons.FileO) },
            { "gtar", ("application/x-gtar", AwesomeIcons.FileO) },
            { "gz", ("application/x-compressed", AwesomeIcons.FileO) },
            { "h", ("text/plain", AwesomeIcons.FileO) },
            { "help", ("application/x-helpfile", AwesomeIcons.FileO) },
            { "hgl", ("application/vnd.hp-hpgl", AwesomeIcons.FileO) },
            { "hh", ("text/plain", AwesomeIcons.FileO) },
            { "hlp", ("application/x-winhelp", AwesomeIcons.FileO) },
            { "htc", ("text/x-component", AwesomeIcons.FileO) },
            { "htm", ("text/html", AwesomeIcons.FileCodeO) },
            { "html", ("text/html", AwesomeIcons.FileCodeO) },
            { "htmls", ("text/html", AwesomeIcons.FileCodeO) },
            { "htt", ("text/webviewhtml", AwesomeIcons.FileCodeO) },
            { "htx", ("text/html", AwesomeIcons.FileCodeO) },
            { "ice", ("x-conference/x-cooltalk", AwesomeIcons.FileO) },
            { "ico", ("image/x-icon", AwesomeIcons.FileO) },
            { "idc", ("text/plain", AwesomeIcons.FileO) },
            { "ief", ("image/ief", AwesomeIcons.FileO) },
            { "iefs", ("image/ief", AwesomeIcons.FileO) },
            { "iges", ("application/iges", AwesomeIcons.FileO) },
            { "igs", ("application/iges", AwesomeIcons.FileO) },
            { "ima", ("application/x-ima", AwesomeIcons.FileO) },
            { "imap", ("application/x-httpd-imap", AwesomeIcons.FileO) },
            { "inf", ("application/inf", AwesomeIcons.FileO) },
            { "ins", ("application/x-internett-signup", AwesomeIcons.FileO) },
            { "ip", ("application/x-ip2", AwesomeIcons.FileO) },
            { "isu", ("video/x-isvideo", AwesomeIcons.FileO) },
            { "it", ("audio/it", AwesomeIcons.FileO) },
            { "iv", ("application/x-inventor", AwesomeIcons.FileO) },
            { "ivr", ("i-world/i-vrml", AwesomeIcons.FileO) },
            { "ivy", ("application/x-livescreen", AwesomeIcons.FileO) },
            { "jam", ("audio/x-jam", AwesomeIcons.FileO) },
            { "jav", ("text/plain", AwesomeIcons.FileO) },
            { "java", ("text/plain", AwesomeIcons.FileO) },
            { "jcm", ("application/x-java-commerce", AwesomeIcons.FileO) },
            { "jfif", ("image/jpeg", AwesomeIcons.FileImageO) },
            { "jfif-tbnl", ("image/jpeg", AwesomeIcons.FileImageO) },
            { "jpe", ("image/jpeg", AwesomeIcons.FileImageO) },
            { "jpeg", ("image/jpeg", AwesomeIcons.FileImageO) },
            { "jpg", ("image/jpeg", AwesomeIcons.FileImageO) },
            { "jps", ("image/x-jps", AwesomeIcons.FileImageO) },
            { "js", ("application/x-javascript", AwesomeIcons.FileO) },
            { "jut", ("image/jutvision", AwesomeIcons.FileO) },
            { "kar", ("audio/midi", AwesomeIcons.FileAudioO) },
            { "ksh", ("application/x-ksh", AwesomeIcons.FileO) },
            { "la", ("audio/nspaudio", AwesomeIcons.FileAudioO) },
            { "lam", ("audio/x-liveaudio", AwesomeIcons.FileAudioO) },
            { "latex", ("application/x-latex", AwesomeIcons.FileCodeO) },
            { "lha", ("application/lha", AwesomeIcons.FileO) },
            { "lhx", ("application/octet-stream", AwesomeIcons.FileO) },
            { "list", ("text/plain", AwesomeIcons.FileO) },
            { "lma", ("audio/nspaudio", AwesomeIcons.FileO) },
            { "log", ("text/plain", AwesomeIcons.FileO) },
            { "lsp", ("application/x-lisp", AwesomeIcons.FileCodeO) },
            { "lst", ("text/plain", AwesomeIcons.FileO) },
            { "lsx", ("text/x-la-asf", AwesomeIcons.FileO) },
            { "ltx", ("application/x-latex", AwesomeIcons.FileCodeO) },
            { "lzh", ("application/octet-stream", AwesomeIcons.FileO) },
            { "lzx", ("application/lzx", AwesomeIcons.FileO) },
            { "m", ("text/plain", AwesomeIcons.FileO) },
            { "m1v", ("video/mpeg", AwesomeIcons.FileVideoO) },
            { "m2a", ("audio/mpeg", AwesomeIcons.FileAudioO) },
            { "m2v", ("video/mpeg", AwesomeIcons.FileVideoO) },
            { "m3u", ("audio/x-mpequrl", AwesomeIcons.FileO) },
            { "man", ("application/x-troff-man", AwesomeIcons.FileO) },
            { "map", ("application/x-navimap", AwesomeIcons.FileO) },
            { "mar", ("text/plain", AwesomeIcons.FileO) },
            { "mbd", ("application/mbedlet", AwesomeIcons.FileO) },
            { "mc$", ("application/x-magic-cap-package-1.0", AwesomeIcons.FileO) },
            { "mcd", ("application/mcad", AwesomeIcons.FileO) },
            { "mcf", ("image/vasa", AwesomeIcons.FileO) },
            { "mcp", ("application/netmc", AwesomeIcons.FileO) },
            { "me", ("application/x-troff-me", AwesomeIcons.FileO) },
            { "mht", ("message/rfc822", AwesomeIcons.FileO) },
            { "mhtml", ("message/rfc822", AwesomeIcons.FileO) },
            { "mid", ("audio/midi", AwesomeIcons.FileAudioO) },
            { "midi", ("audio/midi", AwesomeIcons.FileAudioO) },
            { "mif", ("application/x-frame", AwesomeIcons.FileO) },
            { "mime", ("message/rfc822", AwesomeIcons.FileO) },
            { "mjf", ("audio/x-vnd.audioexplosion.mjuicemediafile", AwesomeIcons.FileAudioO) },
            { "mjpg", ("video/x-motion-jpeg", AwesomeIcons.FileVideoO) },
            { "mm", ("application/base64", AwesomeIcons.FileO) },
            { "mme", ("application/base64", AwesomeIcons.FileO) },
            { "mod", ("audio/mod", AwesomeIcons.FileO) },
            { "moov", ("video/quicktime", AwesomeIcons.FileO) },
            { "mov", ("video/quicktime", AwesomeIcons.FileO) },
            { "movie", ("video/x-sgi-movie", AwesomeIcons.FileO) },
            { "mp2", ("audio/mpeg", AwesomeIcons.FileAudioO) },
            { "mp3", ("audio/mpeg3", AwesomeIcons.FileAudioO) },
            { "mpa", ("audio/mpeg", AwesomeIcons.FileAudioO) },
            { "mpc", ("application/x-project", AwesomeIcons.FileO) },
            { "mpe", ("video/mpeg", AwesomeIcons.FileVideoO) },
            { "mpeg", ("video/mpeg", AwesomeIcons.FileVideoO) },
            { "mpg", ("video/mpeg", AwesomeIcons.FileVideoO) },
            { "mpga", ("audio/mpeg", AwesomeIcons.FileAudioO) },
            { "mpp", ("application/vnd.ms-project", AwesomeIcons.FileO) },
            { "mpt", ("application/x-project", AwesomeIcons.FileO) },
            { "mpv", ("application/x-project", AwesomeIcons.FileO) },
            { "mpx", ("application/x-project", AwesomeIcons.FileO) },
            { "mrc", ("application/marc", AwesomeIcons.FileO) },
            { "ms", ("application/x-troff-ms", AwesomeIcons.FileO) },
            { "mv", ("video/x-sgi-movie", AwesomeIcons.FileVideoO) },
            { "my", ("audio/make", AwesomeIcons.FileAudioO) },
            { "mzz", ("application/x-vnd.audioexplosion.mzz", AwesomeIcons.FileO) },
            { "nap", ("image/naplps", AwesomeIcons.FileO) },
            { "naplps", ("image/naplps", AwesomeIcons.FileO) },
            { "nc", ("application/x-netcdf", AwesomeIcons.FileO) },
            { "ncm", ("application/vnd.nokia.configuration-message", AwesomeIcons.FileO) },
            { "nif", ("image/x-niff", AwesomeIcons.FileImageO) },
            { "niff", ("image/x-niff", AwesomeIcons.FileImageO) },
            { "nix", ("application/x-mix-transfer", AwesomeIcons.FileO) },
            { "nsc", ("application/x-conference", AwesomeIcons.FileO) },
            { "nvd", ("application/x-navidoc", AwesomeIcons.FileO) },
            { "o", ("application/octet-stream", AwesomeIcons.FileO) },
            { "oda", ("application/oda", AwesomeIcons.FileO) },
            { "omc", ("application/x-omc", AwesomeIcons.FileO) },
            { "omcd", ("application/x-omcdatamaker", AwesomeIcons.FileO) },
            { "omcr", ("application/x-omcregerator", AwesomeIcons.FileO) },
            { "p", ("text/x-pascal", AwesomeIcons.FileO) },
            { "p10", ("application/pkcs10", AwesomeIcons.FileO) },
            { "p12", ("application/pkcs-12", AwesomeIcons.FileO) },
            { "p7a", ("application/x-pkcs7-signature", AwesomeIcons.FileO) },
            { "p7c", ("application/pkcs7-mime", AwesomeIcons.FileO) },
            { "pas", ("text/pascal", AwesomeIcons.FileCodeO) },
            { "pbm", ("image/x-portable-bitmap", AwesomeIcons.FileImageO) },
            { "pcl", ("application/vnd.hp-pcl", AwesomeIcons.FileO) },
            { "pct", ("image/x-pict", AwesomeIcons.FileImageO) },
            { "pcx", ("image/x-pcx", AwesomeIcons.FileImageO) },
            { "pdf", ("application/pdf", AwesomeIcons.FilePdfO) },
            { "pfunk", ("audio/make", AwesomeIcons.FileAudioO) },
            { "pgm", ("image/x-portable-graymap", AwesomeIcons.FileImageO) },
            { "pic", ("image/pict", AwesomeIcons.FileImageO) },
            { "pict", ("image/pict", AwesomeIcons.FileImageO) },
            { "pkg", ("application/x-newton-compatible-pkg", AwesomeIcons.FileO) },
            { "pko", ("application/vnd.ms-pki.pko", AwesomeIcons.FileO) },
            { "pl", ("text/plain", AwesomeIcons.FileO) },
            { "plx", ("application/x-pixclscript", AwesomeIcons.FileCodeO) },
            { "pm", ("image/x-xpixmap", AwesomeIcons.FileO) },
            { "png", ("image/png", AwesomeIcons.FileImageO) },
            { "pnm", ("application/x-portable-anymap", AwesomeIcons.FileO) },
            { "pot", ("application/mspowerpoint", AwesomeIcons.FileO) },
            { "pov", ("model/x-pov", AwesomeIcons.FileO) },
            { "ppa", ("application/vnd.ms-powerpoint", AwesomeIcons.FileO) },
            { "ppm", ("image/x-portable-pixmap", AwesomeIcons.FileO) },
            { "pps", ("application/mspowerpoint", AwesomeIcons.FilePowerpointO) },
            { "ppt", ("application/mspowerpoint", AwesomeIcons.FilePowerpointO) },
            { "ppz", ("application/mspowerpoint", AwesomeIcons.FilePowerpointO) },
            { "pre", ("application/x-freelance", AwesomeIcons.FileO) },
            { "prt", ("application/pro_eng", AwesomeIcons.FileO) },
            { "ps", ("application/postscript", AwesomeIcons.FileO) },
            { "psd", ("application/octet-stream", AwesomeIcons.FileO) },
            { "pvu", ("paleovu/x-pv", AwesomeIcons.FileO) },
            { "pwz", ("application/vnd.ms-powerpoint", AwesomeIcons.FilePowerpointO) },
            { "py", ("text/x-script.phyton", AwesomeIcons.FileCodeO) },
            { "pyc", ("applicaiton/x-bytecode.python", AwesomeIcons.FileCodeO) },
            { "qcp", ("audio/vnd.qcelp", AwesomeIcons.FileAudioO) },
            { "qd3", ("x-world/x-3dmf", AwesomeIcons.FileO) },
            { "qd3d", ("x-world/x-3dmf", AwesomeIcons.FileO) },
            { "qif", ("image/x-quicktime", AwesomeIcons.FileO) },
            { "qt", ("video/quicktime", AwesomeIcons.FileVideoO) },
            { "qtc", ("video/x-qtc", AwesomeIcons.FileVideoO) },
            { "qti", ("image/x-quicktime", AwesomeIcons.FileImageO) },
            { "qtif", ("image/x-quicktime", AwesomeIcons.FileImageO) },
            { "ra", ("audio/x-pn-realaudio", AwesomeIcons.FileO) },
            { "ram", ("audio/x-pn-realaudio", AwesomeIcons.FileO) },
            { "ras", ("application/x-cmu-raster", AwesomeIcons.FileO) },
            { "rast", ("image/cmu-raster", AwesomeIcons.FileImageO) },
            { "rexx", ("text/x-script.rexx", AwesomeIcons.FileO) },
            { "rf", ("image/vnd.rn-realflash", AwesomeIcons.FileImageO) },
            { "rgb", ("image/x-rgb", AwesomeIcons.FileImageO) },
            { "rm", ("application/vnd.rn-realmedia", AwesomeIcons.FileO) },
            { "rmi", ("audio/mid", AwesomeIcons.FileAudioO) },
            { "rmm", ("audio/x-pn-realaudio", AwesomeIcons.FileAudioO) },
            { "rmp", ("audio/x-pn-realaudio", AwesomeIcons.FileAudioO) },
            { "rng", ("application/ringing-tones", AwesomeIcons.FileO) },
            { "rnx", ("application/vnd.rn-realplayer", AwesomeIcons.FileO) },
            { "roff", ("application/x-troff", AwesomeIcons.FileO) },
            { "rp", ("image/vnd.rn-realpix", AwesomeIcons.FileImageO) },
            { "rpm", ("audio/x-pn-realaudio-plugin", AwesomeIcons.FileAudioO) },
            { "rt", ("text/richtext", AwesomeIcons.FileWordO) },
            { "rtf", ("text/richtext", AwesomeIcons.FileWordO) },
            { "rtx", ("application/rtf", AwesomeIcons.FileO) },
            { "rv", ("video/vnd.rn-realvideo", AwesomeIcons.FileO) },
            { "s", ("text/x-asm", AwesomeIcons.FileO) },
            { "s3m", ("audio/s3m", AwesomeIcons.FileO) },
            { "saveme", ("application/octet-stream", AwesomeIcons.FileO) },
            { "sbk", ("application/x-tbook", AwesomeIcons.FileO) },
            { "scm", ("application/x-lotusscreencam", AwesomeIcons.FileO) },
            { "sdml", ("text/plain", AwesomeIcons.FileO) },
            { "sdp", ("application/sdp", AwesomeIcons.FileO) },
            { "sdr", ("application/sounder", AwesomeIcons.FileO) },
            { "sea", ("application/sea", AwesomeIcons.FileO) },
            { "set", ("application/set", AwesomeIcons.FileO) },
            { "sgm", ("text/sgml", AwesomeIcons.FileO) },
            { "sgml", ("text/sgml", AwesomeIcons.FileO) },
            { "sh", ("application/x-bsh", AwesomeIcons.Terminal) },
            { "shtml", ("text/html", AwesomeIcons.FileCodeO) },
            { "sid", ("audio/x-psid", AwesomeIcons.FileO) },
            { "sit", ("application/x-sit", AwesomeIcons.FileO) },
            { "skd", ("application/x-koan", AwesomeIcons.FileO) },
            { "skm", ("application/x-koan", AwesomeIcons.FileO) },
            { "skp", ("application/x-koan", AwesomeIcons.FileO) },
            { "skt", ("application/x-koan", AwesomeIcons.FileO) },
            { "sl", ("application/x-seelogo", AwesomeIcons.FileO) },
            { "smi", ("application/smil", AwesomeIcons.FileO) },
            { "smil", ("application/smil", AwesomeIcons.FileO) },
            { "snd", ("audio/basic", AwesomeIcons.FileAudioO) },
            { "sol", ("application/solids", AwesomeIcons.FileO) },
            { "spc", ("application/x-pkcs7-certificates", AwesomeIcons.FileO) },
            { "spl", ("application/futuresplash", AwesomeIcons.FileO) },
            { "spr", ("application/x-sprite", AwesomeIcons.FileO) },
            { "sprite", ("application/x-sprite", AwesomeIcons.FileO) },
            { "src", ("application/x-wais-source", AwesomeIcons.FileO) },
            { "ssi", ("text/x-server-parsed-html", AwesomeIcons.FileO) },
            { "ssm", ("application/streamingmedia", AwesomeIcons.FileO) },
            { "sst", ("application/vnd.ms-pki.certstore", AwesomeIcons.FileO) },
            { "step", ("application/step", AwesomeIcons.FileO) },
            { "stl", ("application/sla", AwesomeIcons.FileO) },
            { "stp", ("application/step", AwesomeIcons.FileO) },
            { "sv4cpio", ("application/x-sv4cpio", AwesomeIcons.FileO) },
            { "sv4crc", ("application/x-sv4crc", AwesomeIcons.FileO) },
            { "svf", ("image/vnd.dwg", AwesomeIcons.FileO) },
            { "svr", ("application/x-world", AwesomeIcons.FileO) },
            { "swf", ("application/x-shockwave-flash", AwesomeIcons.FileO) },
            { "t", ("application/x-troff", AwesomeIcons.FileO) },
            { "talk", ("text/x-speech", AwesomeIcons.FileO) },
            { "tar", ("application/x-tar", AwesomeIcons.FileO) },
            { "tbk", ("application/toolbook", AwesomeIcons.FileO) },
            { "tcl", ("application/x-tcl", AwesomeIcons.FileO) },
            { "tcsh", ("text/x-script.tcsh", AwesomeIcons.FileO) },
            { "tex", ("application/x-tex", AwesomeIcons.FileO) },
            { "texi", ("application/x-texinfo", AwesomeIcons.FileO) },
            { "texinfo", ("application/x-texinfo", AwesomeIcons.FileO) },
            { "text", ("text/plain", AwesomeIcons.FileO) },
            { "tgz", ("application/x-compressed", AwesomeIcons.FileO) },
            { "tif", ("image/tiff", AwesomeIcons.FileO) },
            { "tr", ("application/x-troff", AwesomeIcons.FileO) },
            { "tsi", ("audio/tsp-audio", AwesomeIcons.FileO) },
            { "tsp", ("audio/tsplayer", AwesomeIcons.FileO) },
            { "tsv", ("text/tab-separated-values", AwesomeIcons.FileO) },
            { "turbot", ("image/florian", AwesomeIcons.FileO) },
            { "txt", ("text/plain", AwesomeIcons.FileO) },
            { "uil", ("text/x-uil", AwesomeIcons.FileO) },
            { "uni", ("text/uri-list", AwesomeIcons.FileO) },
            { "unis", ("text/uri-list", AwesomeIcons.FileO) },
            { "unv", ("application/i-deas", AwesomeIcons.FileO) },
            { "uri", ("text/uri-list", AwesomeIcons.FileO) },
            { "uris", ("text/uri-list", AwesomeIcons.FileO) },
            { "ustar", ("application/x-ustar", AwesomeIcons.FileO) },
            { "uu", ("application/octet-stream", AwesomeIcons.FileO) },
            { "vcd", ("application/x-cdlink", AwesomeIcons.FileO) },
            { "vcs", ("text/x-vcalendar", AwesomeIcons.FileO) },
            { "vda", ("application/vda", AwesomeIcons.FileO) },
            { "vdo", ("video/vdo", AwesomeIcons.FileO) },
            { "vew", ("application/groupwise", AwesomeIcons.FileO) },
            { "viv", ("video/vivo", AwesomeIcons.FileO) },
            { "vivo", ("video/vivo", AwesomeIcons.FileO) },
            { "vmd", ("application/vocaltec-media-desc", AwesomeIcons.FileO) },
            { "vmf", ("application/vocaltec-media-file", AwesomeIcons.FileO) },
            { "voc", ("audio/voc", AwesomeIcons.FileO) },
            { "vos", ("video/vosaic", AwesomeIcons.FileO) },
            { "vox", ("audio/voxware", AwesomeIcons.FileO) },
            { "vqe", ("audio/x-twinvq-plugin", AwesomeIcons.FileO) },
            { "vqf", ("audio/x-twinvq", AwesomeIcons.FileO) },
            { "vql", ("audio/x-twinvq-plugin", AwesomeIcons.FileO) },
            { "vrml", ("application/x-vrml", AwesomeIcons.FileO) },
            { "vrt", ("x-world/x-vrt", AwesomeIcons.FileO) },
            { "vsd", ("application/x-visio", AwesomeIcons.FileO) },
            { "vst", ("application/x-visio", AwesomeIcons.FileO) },
            { "vsw", ("application/x-visio", AwesomeIcons.FileO) },
            { "w60", ("application/wordperfect6.0", AwesomeIcons.FileO) },
            { "w61", ("application/wordperfect6.1", AwesomeIcons.FileO) },
            { "w6w", ("application/msword", AwesomeIcons.FileWordO) },
            { "wav", ("audio/wav", AwesomeIcons.FileAudioO) },
            { "wb1", ("application/x-qpro", AwesomeIcons.FileO) },
            { "wbmp", ("image/vnd.wap.wbmp", AwesomeIcons.FileImageO) },
            { "web", ("application/vnd.xara", AwesomeIcons.FileO) },
            { "wiz", ("application/msword", AwesomeIcons.FileO) },
            { "wk1", ("application/x-123", AwesomeIcons.FileO) },
            { "wmf", ("windows/metafile", AwesomeIcons.FileImageO) },
            { "wml", ("text/vnd.wap.wml", AwesomeIcons.FileO) },
            { "wmlc", ("application/vnd.wap.wmlc", AwesomeIcons.FileO) },
            { "wmls", ("text/vnd.wap.wmlscript", AwesomeIcons.FileO) },
            { "wmlsc", ("application/vnd.wap.wmlscriptc", AwesomeIcons.FileO) },
            { "word", ("application/msword", AwesomeIcons.FileO) },
            { "wp", ("application/wordperfect", AwesomeIcons.FileO) },
            { "wp5", ("application/wordperfect", AwesomeIcons.FileO) },
            { "wp6", ("application/wordperfect", AwesomeIcons.FileO) },
            { "wpd", ("application/wordperfect", AwesomeIcons.FileO) },
            { "wq1", ("application/x-lotus", AwesomeIcons.FileO) },
            { "wri", ("application/mswrite", AwesomeIcons.FileO) },
            { "wrl", ("application/x-world", AwesomeIcons.FileO) },
            { "wrz", ("model/vrml", AwesomeIcons.FileO) },
            { "wsc", ("text/scriplet", AwesomeIcons.FileO) },
            { "wsrc", ("application/x-wais-source", AwesomeIcons.FileO) },
            { "wtk", ("application/x-wintalk", AwesomeIcons.FileO) },
            { "xbm", ("image/x-xbitmap", AwesomeIcons.FileO) },
            { "xdr", ("video/x-amt-demorun", AwesomeIcons.FileO) },
            { "xgz", ("xgl/drawing", AwesomeIcons.FileO) },
            { "xif", ("image/vnd.xiff", AwesomeIcons.FileO) },
            { "xl", ("application/excel", AwesomeIcons.FileO) },
            { "xla", ("application/excel", AwesomeIcons.FileO) },
            { "xlb", ("application/excel", AwesomeIcons.FileO) },
            { "xlc", ("application/excel", AwesomeIcons.FileO) },
            { "xld", ("application/excel", AwesomeIcons.FileO) },
            { "xlk", ("application/excel", AwesomeIcons.FileO) },
            { "xll", ("application/excel", AwesomeIcons.FileO) },
            { "xlm", ("application/excel", AwesomeIcons.FileO) },
            { "xls", ("application/excel", AwesomeIcons.FileO) },
            { "xlsx", ("application/excel", AwesomeIcons.FileO) },
            { "xlt", ("application/excel", AwesomeIcons.FileO) },
            { "xlv", ("application/excel", AwesomeIcons.FileO) },
            { "xlw", ("application/excel", AwesomeIcons.FileO) },
            { "xm", ("audio/xm", AwesomeIcons.FileAudioO) },
            { "xml", ("text/xml", AwesomeIcons.FileO) },
            { "xmz", ("xgl/movie", AwesomeIcons.FileO) },
            { "xpix", ("application/x-vnd.ls-xpix", AwesomeIcons.FileO) },
            { "xpm", ("image/x-xpixmap", AwesomeIcons.FileO) },
            { "x-png", ("image/png", AwesomeIcons.FileO) },
            { "xsr", ("video/x-amt-showrun", AwesomeIcons.FileO) },
            { "xwd", ("image/x-xwd", AwesomeIcons.FileO) },
            { "xyz", ("chemical/x-pdb", AwesomeIcons.FileO) },
            { "z", ("application/x-compress", AwesomeIcons.FileO) },
            { "zip", ("application/x-compressed", AwesomeIcons.FileO) },
            { "zoo", ("application/octet-stream", AwesomeIcons.FileO) },
            { "zsh", ("text/x-script.zsh", AwesomeIcons.Terminal) }
        };

    }
    
}


