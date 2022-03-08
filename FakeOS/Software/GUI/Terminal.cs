using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using FakeOS.General;
using FakeOS.Software.CLI;
using FakeOS.Tools;
using ImGuiNET;
using Microsoft.Xna.Framework.Input;

namespace FakeOS.Software.GUI;

public class Terminal : GuiSoftware
{
    private const ImGuiInputTextFlags inputFlags = ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.CallbackCompletion | ImGuiInputTextFlags.CallbackHistory;
    private readonly string binPath = string.Format(".{0}Filesystem{0}sys{0}bin", Path.DirectorySeparatorChar);
    
    private readonly List<string> text = new List<string>();
    private List<string> history = new List<string>();
    
    private readonly Dictionary<string, Action<List<string>>> builtInCommands = new Dictionary<string, Action<List<string>>>();
    private readonly Dictionary<string, ConstructorInfo> binCommands = new Dictionary<string, ConstructorInfo>();

    private StringCompletion completion;
    private string inputText = "";
    
    public Terminal(List<string> args = null) : base(args)
    {
        fancyName = "Terminal";

        addBinCommands();
        addBuiltinCommands();

        List<string> keys = builtInCommands.Keys.Concat(binCommands.Keys).ToList();

        completion = new StringCompletion(new List<string>(keys));
    }

    #region mainUpdateLoops
    
    public override unsafe void imGuiUpdate()
    {
        if (!running) return;

        if (ImGui.Begin(fancyName, ref running))
        {
            if(ImGui.Button("Add text")) text.Add("Test");
            
            ImGui.BeginChild("#main", new Vector2(0, -35), true);
            
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

            ImGui.InputText("Input", ref inputText, byte.MaxValue, inputFlags, inputCallback);  
            
            ImGui.End();
            
        }
    }

    #endregion

    private unsafe int inputCallback(ImGuiInputTextCallbackData* data)
    {
        
        
        return 0;
    }

    #region Add Commands

    private void addBinCommands()
    {
        string[] dummyFiles = Directory.GetFiles(binPath);
        Dictionary<string, string> commandStubs = dummyFiles.ToDictionary(Path.GetFileNameWithoutExtension, FileReader.getFileString);

        foreach (var commandStub in commandStubs)
        {
            Type type = getTypeOfSoftware(commandStub.Value);
            Type[] argTypes = { typeof(List<string>) };
            
            ConstructorInfo constructor = type.GetConstructor(argTypes);


            binCommands.Add(commandStub.Key, constructor);
        }
        
        // constructor!.Invoke(new object[1] { new List<string>() });
        // foreach (var programName in fileContents) TODO
        // {
        //     you need to provide full path here
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
        builtInCommands.Add("cd", changeDirectory);
        builtInCommands.Add("help", help);
    }
    
    #endregion
    
    #region Built-in Commands

    private void changeDirectory(List<string> args)
    {
        
    }

    private void help(List<string> args)
    {
        
    }
    
    #endregion

    #region util

    private Type getTypeOfSoftware(string name)
    {
        Type rv;
            
        try
        {
            var guiType = typeof(GuiSoftware);
            rv = Type.GetType(guiType.Namespace + '.' + name, true);
        }
        catch (Exception)
        {
            var cliType = typeof(CliSoftware);
            rv = Type.GetType(cliType.Namespace + '.' + name, true);
        }

        return rv;
    }

    #endregion
};
