using System;
using System.IO;
using System.Numerics;

namespace FakeOS;

public static class Consts
{
   public static (string, int) startupFont = (
       
      String.Format(".{0}Filesystem{0}sys{0}fonts{0}JetBrainsMono-Medium.ttf", Path.DirectorySeparatorChar),
      20
      
      );
   

   public static readonly string filesystemPrefix = ($".{Path.DirectorySeparatorChar}Filesystem");
   public static readonly string binLocation = (String.Format(".{0}Filesystem{0}sys{0}bin", Path.DirectorySeparatorChar));
   public static readonly string themeLocation = (String.Format(".{0}Filesystem{0}sys{0}themes", Path.DirectorySeparatorChar));
   public static readonly string fontsLocation = (String.Format(".{0}Filesystem{0}sys{0}fonts", Path.DirectorySeparatorChar));
   public static readonly string fontAwesomeLocation = (String.Format(".{0}Filesystem{0}sys{0}fonts{0}misc{0}fontawesome-webfont.ttf", Path.DirectorySeparatorChar));

   public const int defaultFontSize = 20;
   
   public const string folderType = "misc/directory";
   
   public static readonly Vector4 highlightColor = new Vector4(255, 218, 57, 255);
} 