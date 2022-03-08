using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using FakeOS.Tools;
using ImGuiNET;

namespace FakeOS.Software.GUI;

public class Terminal : GuiSoftware
{
    private const ImGuiInputTextFlags inputFlags = ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.CallbackCompletion | ImGuiInputTextFlags.CallbackHistory;
    private readonly string binPath = string.Format(".{0}Filesystem{0}sys{0}bin", Path.DirectorySeparatorChar);
    
    private readonly List<string> text = new List<string>();
    private List<string> history = new List<string>();
    private readonly Dictionary<string, Action<List<string>>> commands = new Dictionary<string, Action<List<string>>>();

    private string inputText = "";
    
    public Terminal(List<string> args = null) : base(args)
    {
        fancyName = "Terminal";
        
        addBinCommands();
        addBuiltinCommands();
    }

    #region mainUpdateLoops
    
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

            ImGui.InputText("Input", ref inputText, (uint)inputText.Length + 1, inputFlags);  
            
            ImGui.End();
            
        }
    }

    #endregion

    private void addBinCommands()
    {
        string[] dummyFiles = Directory.GetFiles(binPath);
        Dictionary<string, string> commandStubs = dummyFiles.ToDictionary(Path.GetFileNameWithoutExtension, FileReader.getFileString);

        foreach (var commandStub in commandStubs)
        {
            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(commandStub.Value);
            Action<List<string>> action = (Action<List<string>>) Delegate.CreateDelegate(typeof(Action<List<string>>), theMethod!);
            
            commands.Add(commandStub.Key, action);
        }
        
        // foreach (var programName in fileContents) TODO
        // {
        //     Type type = Type.GetType(programName);
        //
        //     if (type is GuiSoftware)
        //     {
        //         Game1.windows.Add((GuiSoftware)Activator.CreateInstance(type!));
        //     }
        //     else if (type is CliSoftware)
        //     {
        //         return;
        //     }
        //     else
        //     {
        //         throw new Exception("Unknown type.");
        //     }
        // }
    }

    private void addBuiltinCommands()
    {
        commands.Add("cd", changeDirectory);
        commands.Add("help", help);
    }
    
    #region Built-in Commands

    private void changeDirectory(List<string> args)
    {
        
    }

    private void help(List<string> args)
    {
        
    }
    
    #endregion
};
