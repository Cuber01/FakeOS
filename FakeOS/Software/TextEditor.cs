using System;
using System.Numerics;
using ImGuiNET;

namespace FakeOS.Software;

public class TextEditor : GuiSoftware
{
    private string text = "";
    private readonly ImGuiInputTextFlags multilineTextFlags = ImGuiInputTextFlags.CallbackCharFilter | ImGuiInputTextFlags.AllowTabInput;
    
    public TextEditor()
    {
        this.name = "Text Editor";
        running = true;
    }
    
    public unsafe override void draw()
    {
        ImGui.Begin(name, ref running);

        ImGui.InputTextMultiline("", ref text, UInt16.MaxValue, new Vector2(1000, 1000),
            multilineTextFlags, processData);

        ImGui.End();
    }

    private unsafe int processData(ImGuiInputTextCallbackData* data)
    {
        return 0;
    }
    
}