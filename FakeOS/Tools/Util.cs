using System;
using System.Collections.Generic;
using FakeOS.Software.GUI;

namespace FakeOS.Tools;

public static class Util
{
    public static object getField(object src, string fieldName) => src.GetType().GetField(fieldName)!.GetValue(src)!;

    public static void openFileInNewTextEditor(string path)
    {
        Game1.windows.Add(new TextEditor(new List<string> { path }, null));
    }

    public static string removeAfterCharacter(string input, char character)
    {
        int index = input.LastIndexOf(character);

        if (index >= 0)
        {
            input = input.Substring(0, index + 1);
        }
        else
        {
            throw new Exception("Character not found.");
        }

        return input;
    }

}