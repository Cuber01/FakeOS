using System;
using System.IO;

namespace FakeOS;

public static class Consts
{
   public static (string, int) startupFont = (
       
      String.Format(".{0}Filesystem{0}sys{0}fonts{0}JetBrainsMono-Medium.ttf", Path.DirectorySeparatorChar),
      20
      
      );
   
   public static string filesystemLocation = (String.Format(".{0}Filesystem{0}", Path.DirectorySeparatorChar));
   public static readonly string themeLocation = (String.Format(".{0}Filesystem{0}sys{0}themes", Path.DirectorySeparatorChar));
   public static readonly string fontsLocation = (String.Format(".{0}Filesystem{0}sys{0}fonts", Path.DirectorySeparatorChar));
   public static readonly string fontAwesomeLocation = (String.Format(".{0}Filesystem{0}sys{0}fonts{0}misc{0}fontawesome-webfont.ttf", Path.DirectorySeparatorChar));

   public const int defaultFontSize = 20;
   
   public const string folderType = "misc/directory";
} 