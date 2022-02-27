using FakeOS.Software;

namespace FakeOS.Tools;

public static class Util
{
    public static object getField(object src, string fieldName) => src.GetType().GetField(fieldName)!.GetValue(src)!;

    public static void openFileInNewTextEditor(string path)
    {
        Game1.windows.Add(new TextEditor());
    }
}