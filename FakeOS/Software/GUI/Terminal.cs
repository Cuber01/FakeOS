using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using FakeOS.Software.CLI;
using FakeOS.Tools;
using ImGuiNET;

namespace FakeOS.Software.GUI;

public class Terminal : GuiSoftware
{
    private const ImGuiInputTextFlags inputFlags = ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.CallbackCompletion | ImGuiInputTextFlags.CallbackHistory;
    private readonly string binPath = string.Format(".{0}Filesystem{0}sys{0}bin", Path.DirectorySeparatorChar);
    
    private List<string> text = new List<string>();
    private List<string> history = new List<string>();

    private string inputText = "";
    
    public Terminal()
    {
        fancyName = "Terminal";
        
        getCommands();
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

        if (ImGui.Begin(fancyName, ref running))
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

    private void getCommands()
    {
        string[] dummyFiles = Directory.GetFiles(binPath);
        List<string> fileContents = dummyFiles.Select(FileReader.getFileString).ToList();

        foreach (var programName in fileContents)
        {
            Type type = Type.GetType(programName);

            if (type is GuiSoftware)
            {
                Game1.windows.Add((GuiSoftware)Activator.CreateInstance(type!));
            }
            else if (type is CliSoftware)
            {
                return;
            }
            else
            {
                throw new Exception("Unknown type.");
            }
        }
    }
};
