using System;
using System.Collections.Generic;
using System.Numerics;
using ImGuiNET;

namespace FakeOS.Software.GUI;

public class Terminal : GuiSoftware
{
    private List<string> text = new List<string>();
    private List<string> history = new List<string>();

    private string inputText = "";

    private const ImGuiInputTextFlags inputFlags = ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.CallbackCompletion | ImGuiInputTextFlags.CallbackHistory;

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
            if(ImGui.Button("Add text")) text.Add("Test");
            
            ImGui.BeginChild("#main", new Vector2(0, -35), false, ImGuiWindowFlags.HorizontalScrollbar);
            
            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(4, 1)); // Tighten spacing

            foreach (var entry in text)
            {
                ImGui.Text(entry);    
            }
            
            // Auto scroll
            if(ImGui.GetScrollY() >= ImGui.GetScrollMaxY())
            {
                ImGui.SetScrollHereY(1.0f);    
            }
            
            ImGui.PopStyleVar();
            
            ImGui.EndChild(); 
            
            ImGui.Separator();

            ImGui.InputText("Input", ref inputText, byte.MaxValue, inputFlags);  
            
            ImGui.End();
            
        }
    }

    #endregion
};
