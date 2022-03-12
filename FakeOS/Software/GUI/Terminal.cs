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
    // flags
    private const ImGuiInputTextFlags inputFlags = ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.CallbackCompletion | ImGuiInputTextFlags.CallbackHistory;

    // input, output
    private string inputText = "";
    private readonly List<string> consoleOutput = new List<string>();
    
    // history
    private readonly Stack<string> history = new Stack<string>();
    private int currentHistoryPos = -1;

    // command lists
    private readonly Dictionary<string, Action<List<string>>> builtInCommands = new Dictionary<string, Action<List<string>>>();
    private Dictionary<string, string> binCommands = new Dictionary<string, string>();

    // other
    private readonly StringCompletion completionModule;

    public Terminal(Action<string> echo, List<string> args = null) : base(args, echo)
    {
        fancyName = "Terminal";

        addBinCommands();
        addBuiltinCommands();

        List<string> keys = builtInCommands.Keys.Concat(binCommands.Keys).ToList();

        completionModule = new StringCompletion(new List<string>(keys));
    }
    
    
    #region mainUpdateLoops

    public override unsafe void imGuiUpdate()
    {
        if (!running) return;

        if (ImGui.Begin(fancyName, ref running))
        {

            ImGui.BeginChild("#main", new Vector2(0, -35), true); // TODO this has to be calculated
            
            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(4, 1)); // Tighten spacing

            foreach (var entry in consoleOutput)
            {
                if (entry.StartsWith('#'))
                {
                    ImGui.PushStyleColor(ImGuiCol.Text, Consts.highlightColor); // TODO this doesnt work
                    
                    ImGui.TextUnformatted(entry);        
                    
                    ImGui.PopStyleColor();
                }
                else
                {
                    ImGui.Text(entry);        
                }
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

                string newText = completionModule.complete(inputText);
                replaceInput(data, newText);
                
                break;
            }
            
            case ImGuiInputTextFlags.CallbackHistory:
            {
                if (data->EventKey == ImGuiKey.UpArrow)
                {
                    if (currentHistoryPos < history.Count - 1)
                    {
                        currentHistoryPos++;
                        replaceInput(data, history.ElementAt(currentHistoryPos));
                    }
                } 
                else if (data->EventKey == ImGuiKey.DownArrow)
                {
                    if (currentHistoryPos > -1)
                    {
                        currentHistoryPos--;

                        if (currentHistoryPos != -1)
                        {
                            replaceInput(data, history.ElementAt(currentHistoryPos));    
                        }
                        else
                        {
                            replaceInput(data, "");
                        }
                        
                    }
                }

                break;    
            }
            
        }

        return 0;
    }

    #endregion

    #region Add Commands

    private void addBinCommands()
    {
        string[] dummyFiles = Directory.GetFiles(Consts.binLocation);
        binCommands = dummyFiles.ToDictionary(Path.GetFileNameWithoutExtension, FileReader.getFileString);
    }

    private void addBuiltinCommands()
    {
        builtInCommands.Add("cd", changeDirectory);
        builtInCommands.Add("help", help);
        builtInCommands.Add("echo", echol);
    }
    
    #endregion
    
    #region Built-in Commands

    private void changeDirectory(List<string> args)
    {
        
    }

    private void help(List<string> args)
    {
        if (args.Count is 0)
        {
            echo("Fake Shell\n");
            
            echo("");

            echo("Built-in commands:");
            foreach (var entry in builtInCommands)
            {
                echo("- " + entry.Key);
            }

            echo("");

            echo("For a list of all non-built-in commands run: help --list-bin");
        }
        else if(args.ElementAt(0) == "--list-bin")
        {
            echo("Fake Shell");
            
            echo("");

            echo("Additional commands:");
            foreach (var entry in binCommands)
            {
                echo("- " + entry.Key);
            }
        }
        else
        {
            echo("Unknown argument: " + args.ElementAt(0));
        }
    }
    
    private void echol(List<string> args)
    {
        string message = String.Join(' ', args);
        
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
        history.Push(inputText);
        currentHistoryPos = -1;

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
                    execCommand(command, false);
                    commandFound = true;
                    
                    break;
                }
            }

            if (!commandFound)
            {
                        
                foreach (var entry in builtInCommands)
                {
                    if (command.ElementAt(0) == entry.Key)
                    {
                        execCommand(command, true);
                        commandFound = true;
                        
                        break;
                    }
                }
                        
            }

            if (!commandFound)
            {
                echo("Unknown command: " + inputText);
            }

        }
    }
    
    private void execCommand(List<string> command, bool builtin)
    {
        echo("# " + command.ElementAt(0));
        
        // Init args
        string commandName = command.ElementAt(0);
        
        List<string> args = command;
        args.Remove(commandName);

        if (builtin)
        {
            bool success = builtInCommands.TryGetValue(commandName, out var action);

            if (!success) throw new Exception("Do smth here");
            
            action.Invoke(args);
        }
        else
        {
            bool success = binCommands.TryGetValue(commandName, out var className);

            if (!success) throw new Exception("Do smth here");
            //
            // constructor.Invoke(new object[] { args });

            // you need to provide full path here
            Type type = getTypeOfSoftware(className);

            if (type.AssemblyQualifiedName!.Contains("FakeOS.Software.GUI"))
            {
                Game1.windows.Add((GuiSoftware)Activator.CreateInstance(type!, args, null));
                echo("Opening a window...");
            }
            else if (type.AssemblyQualifiedName!.Contains("FakeOS.Software.CLI"))
            {
                var unused = (CliSoftware)Activator.CreateInstance(type!, args, echo);
            }
            else
            {
                throw new Exception("Unknown type.");
            }
        }
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
