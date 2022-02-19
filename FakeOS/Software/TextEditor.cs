using System;
using System.Numerics;
using ImGuiNET;

namespace FakeOS.Software;

public class TextEditor : GuiSoftware
{
    private string text = "";
    private readonly ImGuiInputTextFlags multilineTextFlags = ImGuiInputTextFlags.CallbackCharFilter | ImGuiInputTextFlags.AllowTabInput;
    private readonly ImGuiWindowFlags windowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.MenuBar;

    private bool pushedColor = false;
    
    public TextEditor()
    {
        this.name = "Text Editor";
        running = true;
    }
    

    public unsafe override void draw()
    {
        ImGui.Begin(name, ref running, windowFlags);

        ImGui.InputTextMultiline("", ref text, UInt16.MaxValue, new Vector2(1000, 1000),
            multilineTextFlags, processData);
        
        ImGui.End();
    }

    private unsafe int processData(ImGuiInputTextCallbackData* data)
    {
        return 0;
    }
    
}