using System;
using System.Collections.Generic;
using ImGuiNET;

namespace FakeOS.Software.GUI;

public class SystemBar : GuiSoftware
{
    public SystemBar(List<string> args, Action<string> echo = null, string executionDirectory = null) : base(args, echo, executionDirectory) { }
    
    public override void imGuiUpdate()
    {
        base.imGuiUpdate();
        
        if(ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Menu"))
            {
                if(ImGui.BeginMenu("Apps"))
                {
                    ImGui.MenuItem("Item");
                    ImGui.MenuItem("Item");
                    ImGui.MenuItem("Item");
                    ImGui.MenuItem("Item");
                    ImGui.MenuItem("Item");
                    ImGui.EndMenu();
                }
                ImGui.EndMenu();
            }
        
            ImGui.EndMainMenuBar();
        }
    }
}