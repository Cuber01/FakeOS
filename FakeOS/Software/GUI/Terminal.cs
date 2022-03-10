using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using FakeOS.General;
using FakeOS.Software.CLI;
using FakeOS.Tools;
using ImGuiNET;

namespace FakeOS.Software.GUI;

public class Terminal : GuiSoftware
{
    private const ImGuiInputTextFlags inputFlags = ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.CallbackCompletion | ImGuiInputTextFlags.CallbackHistory;
    private readonly string binPath = string.Format(".{0}Filesystem{0}sys{0}bin", Path.DirectorySeparatorChar);
    
    private readonly List<string> consoleOutput = new List<string>();
    
    private List<string> history = new List<string>();
    private int currentHistoryPos;

    private readonly Dictionary<string, Action<List<string>>> builtInCommands = new Dictionary<string, Action<List<string>>>();
    private readonly Dictionary<string, ConstructorInfo> binCommands = new Dictionary<string, ConstructorInfo>();

    private readonly StringCompletion completion;
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

            if(ImGui.Button("Add text")) consoleOutput.Add("Test");
            
            ImGui.BeginChild("#main", new Vector2(0, -35), true); // TODO this has to be calculated
            
            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(4, 1)); // Tighten spacing

            foreach (var entry in consoleOutput)
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
            
            if (ImGui.InputText("Input", ref inputText, byte.MaxValue, inputFlags, inputCallback))
            {
                submitCommand();    
             
                // While the focus is gone we can use the chance to easily clear inputText
                inputText = String.Empty;
                
                // ImGui removes keyboard focus after pressing enter by default, -1 sets it back to the last element touched
                ImGui.SetKeyboardFocusHere(-1);
            }
            
            ImGui.End();
            
        }

    }

    private unsafe int inputCallback(ImGuiInputTextCallbackData* data)
    {
        switch (data->EventFlag)
        {
            case ImGuiInputTextFlags.CallbackCompletion:
            {
                // We don't auto complete args and all other commands containing a space are invalid
                if (inputText.Contains(' ')) return 0;

                string newText = completion.complete(inputText);
                replaceInput(data, newText);
                
                break;
            }
            
            case ImGuiInputTextFlags.CallbackHistory:
            {
                if (data->EventKey == ImGuiKey.UpArrow)
                {
                    if (currentHistoryPos > -1)
                    {
                        currentHistoryPos--;
                    }
                } 
                else if (data->EventKey == ImGuiKey.DownArrow)
                {
                    if (currentHistoryPos < history.Count - 1)
                    {
                        currentHistoryPos++;
                    }
                }

                try
                {
                    replaceInput(data, history.ElementAt(currentHistoryPos));
                }
                catch (Exception e) { /* Ignored */ }


                break;    
            }
            
        }

        return 0;
    }

    #endregion

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
    
    private void echo(List<string> args)
    {
        string message = String.Join(String.Empty, args);
        
        consoleOutput.Add(message);
    }
    
    private void echo(string message)
    {
        consoleOutput.Add(message);
    }

    
    #endregion

    #region handling commands

    private void submitCommand()
    {
        history.Add(inputText);
        currentHistoryPos = history.Count - 1;
        
        if (inputText.Length != 0)
        {
            string cleanedInput = inputText.Trim();
            cleanedInput = removeCloseDuplicates(cleanedInput, ' ');
                    
            List<string> command = cleanedInput.Split(' ').ToList();
            bool commandFound = false;

            foreach (var entry in binCommands)
            {
                if (command.ElementAt(0) == entry.Key)
                {
                    execCommand(command);
                    commandFound = true;
                }
            }

            if (!commandFound)
            {
                        
                foreach (var entry in builtInCommands)
                {
                    if (command.ElementAt(0) == entry.Key)
                    {
                        execCommand(command);
                    }
                }
                        
            }

            if (!commandFound)
            {
                echo("Unknown command: " + inputText);
            }

        }
    }
    
    private void execCommand(List<string> command)
    {
        echo(command.ElementAt(0));
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

    private unsafe void replaceInput(ImGuiInputTextCallbackData* data, string newText)
    {
        
        Marshal.Copy(Encoding.UTF8.GetBytes(newText), 0, (IntPtr)data->Buf, newText.Length);
        data->BufTextLen = newText.Length;
        data->BufSize = newText.Length;
        data->BufDirty = 1;
        data->CursorPos = data->SelectionStart = data->SelectionEnd = newText.Length;
        
    }

    private string removeCloseDuplicates(string s, char charToRemove)
    {
        StringBuilder rv = new StringBuilder(s);
        
        for (int i = 0; i < rv.Length - 1; i++)
        {
            if (rv[i] == charToRemove)
            {
                if (rv[i + 1] == charToRemove)
                {
                    rv.Remove(i + 1, 1);
                }
            }
        }

        return rv.ToString();
    }
    
    #endregion

};
