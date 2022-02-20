using System;
using System.IO;

namespace FakeOS;

public static class Consts
{
   public static (string, int) startupFont = (
       
      String.Format(".{0}Filesystem{0}sys{0}fonts{0}Roboto-Regular.ttf", Path.DirectorySeparatorChar),
      18
      
      );
}