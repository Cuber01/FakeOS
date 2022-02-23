using System;
using System.IO;

namespace FakeOS;

public static class Consts
{
   public static (string, int) startupFont = (
       
      String.Format(".{0}Filesystem{0}sys{0}fonts{0}JetBrainsMono-Medium.ttf", Path.DirectorySeparatorChar),
      20
      
      );
   
   public static string themeLocation = (String.Format(".{0}Filesystem{0}sys{0}themes", Path.DirectorySeparatorChar));
   public static string fontsLocation = (String.Format(".{0}Filesystem{0}sys{0}fonts", Path.DirectorySeparatorChar));
   public static string fontAwesomeLocation = (String.Format(".{0}Filesystem{0}sys{0}fonts{0}misc{0}fontawesome-webfont.ttf", Path.DirectorySeparatorChar));

   public const int defaultFontSize = 20;
} 