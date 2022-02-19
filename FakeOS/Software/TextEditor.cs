using System;
using System.Numerics;
using ImGuiNET;

namespace FakeOS.Software;

public class TextEditor : GuiSoftware
{
    private string text = "";
    private readonly ImGuiInputTextFlags multilineTextFlags = ImGuiInputTextFlags.AllowTabInput;
    
    public TextEditor()
    {
        this.name = "Text Editor";
        running = true;
    }
    
    public override void draw()
    {
        ImGui.Begin(name, ref running);
       
        ImGui.InputTextMultiline("", ref text, UInt32.MaxValue, new Vector2(400,  ImGui.GetTextLineHeight() * 16), 
            multilineTextFlags);

        ImGui.End();
    }
}