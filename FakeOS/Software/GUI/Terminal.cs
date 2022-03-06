using System;
using System.Numerics;
using ImGuiNET;

namespace FakeOS.Software.GUI;

public class Terminal : GuiSoftware
{
    
    private string inputText = "";

    private const ImGuiInputTextFlags outputFlags = ImGuiInputTextFlags.ReadOnly;
    private const ImGuiInputTextFlags inputFlags = ImGuiInputTextFlags.None;
    
    public Terminal()
    {
        name = "Terminal";
    }

    #region mainUpdateLoops

    public override void update()
    {
        if (!running) return;
    }

    
    private readonly float footerHeightToReserve = ImGui.GetStyle().ItemSpacing.Y + ImGui.GetFrameHeightWithSpacing();
    public override void imGuiUpdate()
    {
        if (!running) return;

        if (ImGui.Begin(name, ref running))
        {
            
            ImGui.BeginChild("ScrollingRegion", new Vector2(0, -30), false, ImGuiWindowFlags.HorizontalScrollbar);
            
            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(4, 1)); // Tighten spacing
            
            ImGui.Text("Hello");
            ImGui.Text("Hello");
            
            ImGui.PopStyleVar();
            
            ImGui.EndChild(); 
            
            ImGui.Separator();

            ImGui.InputText("Input", ref inputText, byte.MaxValue, inputFlags);  
            
            ImGui.End();
            
        }
    }

    #endregion
};
