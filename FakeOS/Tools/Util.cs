namespace FakeOS.Tools;

public static class Util
{
    public static object getField(object src, string fieldName) => src.GetType().GetField(fieldName)!.GetValue(src)!;


}