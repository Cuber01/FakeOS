using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using FakeOS.General;
using FakeOS.Software.CLI;
using FakeOS.Tools;
using ImGuiNET;

namespace FakeOS.Software.GUI;

public class FileManager : GuiSoftware
{
    #region fields

    private readonly  Dictionary<string, (string, string)> filesAndTypes = new Dictionary<string, (string, string)>();
    private           Dictionary<string, bool>             selected      = new Dictionary<string, bool>();
    
    private           Dictionary<string, Action<string>>   doubleClickActions;
    private readonly  List<string>                         goingBackHistory = new List<string>();

    private string    currentPathInputField;
    private string    currentPath;

    private const int framesBetweenFileChecks = 500;
    private int       fileCheckTimer;

    private const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.MenuBar;
    private const ImGuiTableFlags tableFlags = ImGuiTableFlags.BordersOuter | ImGuiTableFlags.Resizable | ImGuiTableFlags.Sortable;

    private const string filePopupID = "#rcFile";
    private const string backgroundPopupID = "#rcBackground";
    
    private readonly string filesystemPrefix = $".{Path.DirectorySeparatorChar}Filesystem";

    // If true, the file should be removed upon pasting
    private readonly Dictionary<string, bool> filesToCopyCut = new Dictionary<string, bool>();
    
    #endregion

    public FileManager(List<string> args, Action<string> echo) : base(args, echo)
    {
        fancyName = "File Manager";
        
        this.currentPath = args.ElementAt(0);
        
        updateInputPath();

        initDoubleClickActions();
        getFilesAndTypes(currentPath);
    }

    #region mainUpdateLoops

    public override void imGuiUpdate()
    {
        if (!running) return;

        ImGui.Begin(fancyName, ref running, windowFlags);

        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(2, 2));
        
        // Check for right click on background, if it hits a file, it will be overriden by the file popup anyway
        if(ImGui.GetIO().MouseClicked[1])
        {
            ImGui.OpenPopup(backgroundPopupID);
        }
        
        backgroundPopupUpdate();
        
        showUpperUI();
        
        showTable();

        ImGui.PopStyleVar();

        ImGui.End();
    }

    public override void update()
    {
        if (!running) return;

        fileCheckTimer++;
        if (fileCheckTimer == framesBetweenFileChecks)
        {
            fileCheckTimer = 0;
            getFilesAndTypes(currentPath);
        }
    }
    
    #endregion
    
    #region fileView

    private void showTable()
    {
        if (ImGui.BeginTable("#main", 1, tableFlags))
        {
            showFiles(0);

            ImGui.EndTable();
        }
    }

    private void showFiles(int id)
    {

        // Use object uid as identifier. Most commonly you could also use the object pointer as a base ID.
        ImGui.PushID(id);

        // Text and Tree nodes are less high than framed widgets, using AlignTextToFramePadding() we add vertical spacing to make the tree lines equal high.
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.AlignTextToFramePadding();

        for (int i = 0; i < filesAndTypes.Count; i++)
        {
            var fileKey = filesAndTypes.Keys.ElementAt(i);
            var fileVal = filesAndTypes.Values.ElementAt(i);
            
            if (fileKey.Contains(currentPath))
            {
                handleFile((fileKey, fileVal.Item1, fileVal.Item2), i);
            }
        }


        ImGui.PopID();

    }

    // Item1 is path, Item2 is type, Item3 is icon
    private void handleFile((string, string, string) file, int id)
    {
        ImGui.PushID(id);

        bool isSelected = selected[file.Item1];
        
        ImGui.AlignTextToFramePadding();

        if (ImGui.Selectable(file.Item3 + ' ' + Path.GetFileName(file.Item1), ref isSelected))
        {
            if (!ImGui.GetIO().KeyCtrl)
            {
                isSelected = false;

                selected = selected.Keys.ToDictionary(x => x, _ => false);
            }
        }

        if (ImGui.IsItemHovered() && ImGui.IsMouseDoubleClicked(0))
        {
            openFile((file.Item1, file.Item2));
        } 
        else
        if (ImGui.IsItemHovered() && ImGui.GetIO().MouseClicked[1])
        {
            ImGui.OpenPopup(filePopupID);
        }
        
        filePopupUpdate((file.Item1, file.Item2));
        
        selected[file.Item1] = isSelected;

        ImGui.PopID();
    }

    // Item1 is path, Item2 is type
    private void openFile((string, string) file)
    {
        foreach (var entry in doubleClickActions)
        {
            if (file.Item2.Contains(entry.Key))
            {
                entry.Value.Invoke(file.Item1);
            }
        }
    }
    
    private void getFilesAndTypes(string directory)
    {
        filesAndTypes.Clear();
        selected.Clear();

        foreach (string file in Directory.GetFileSystemEntries(directory))
        {
            filesAndTypes.Add(file, MimeTypes.GetContentType(file));
        }

        foreach (var file in filesAndTypes)
        {
            selected.Add(file.Key, false);
        }
    }
    
    #endregion
    
    #region upperUIBar

    private unsafe void showUpperUI()
    {
        if (ImGui.Button(AwesomeIcons.ArrowLeft, new Vector2(25, 25)))
        {
            goBack();
        }
        
        ImGui.SameLine();

        if (ImGui.Button(AwesomeIcons.ArrowRight, new Vector2(25, 25)))
        {
            reverseGoingBack();
        }
        
        ImGui.SameLine();

        ImGui.InputText("", ref currentPathInputField, UInt16.MaxValue, ImGuiInputTextFlags.CallbackAlways,
            _ =>
            {
                if (ImGui.IsKeyDown(ImGuiKey.Enter))
                {
                    // Submit the path on enter press
                    tryUseTypedPath();
                }

                return 0;
            }

        );
    }

    private void goBack()
    {
        // Remove the last /
        string tmpPath = currentPath.Substring(0, currentPath.Length - 1);
        
        // Return if we can't go back further
        if (tmpPath == filesystemPrefix)
        {
            return;
        }

        // Save the previous path
        goingBackHistory.Add(currentPath);
        
        currentPath = tmpPath;
        currentPath = Util.removeAfterCharacter(currentPath, Path.DirectorySeparatorChar);

        updateInputPath();
        getFilesAndTypes(currentPath);
    }

    private void reverseGoingBack()
    {
        if(goingBackHistory.Count == 0) return;
        
        currentPath = goingBackHistory.ElementAt(goingBackHistory.Count - 1);
        goingBackHistory.RemoveAt(goingBackHistory.Count - 1);
        
        updateInputPath();
        getFilesAndTypes(currentPath);
    }

    private void tryUseTypedPath()
    {
        if (Directory.Exists(filesystemPrefix + currentPathInputField))
        {
            useTypedPath();
            return;
        }
        
        // Remove some bullshit after the last slash if necessary
        currentPathInputField = Util.removeAfterCharacter(currentPathInputField, Path.DirectorySeparatorChar);
        
        // Check without the trash after slash
        if (Directory.Exists(filesystemPrefix + currentPathInputField))
        {
            useTypedPath();
        }
    }

    private void useTypedPath()
    {
        currentPath = currentPathInputField;
        currentPath = currentPath.Insert(0, filesystemPrefix);
            
        getFilesAndTypes(currentPath);
    }
    
    #endregion
    
    #region rightClickMenus

    private void filePopupUpdate((string, string) file)
    {
        
        if (ImGui.BeginPopup(filePopupID))
        {

            if (ImGui.Selectable("Open  ")) openFile(file);

            ImGui.Separator();
            
            if (ImGui.Selectable("Cut  ")) copy(file, true);

            if (ImGui.Selectable("Copy  ")) copy(file, false);

            ImGui.Separator();
            
            if (ImGui.Selectable("Delete  ")) delete(file);

            ImGui.EndPopup();
        }
        
        
    }
    
    private void backgroundPopupUpdate()
    {
        
        if (ImGui.BeginPopup(backgroundPopupID))
        {
            if (ImGui.Selectable("New Text File  ")) newTextFile();

            if (ImGui.Selectable("New Folder  ")) newDirectory();

            ImGui.Separator();
            
            if (ImGui.Selectable("Paste  ")) paste();

            ImGui.Separator();
            
            if (ImGui.Selectable("Open in Terminal  "))
            {
                
            }

            ImGui.EndPopup();
        }
        
        
    }

    #region popupActions

    private void copy((string, string) file, bool cut)
    {
        filesToCopyCut.Add(file.Item1, cut);

        foreach (var entry in 
                 selected.Where(entry => !filesToCopyCut.ContainsKey(entry.Key)))
        {
            if(entry.Value is not true) continue;
            
            filesToCopyCut.Add(entry.Key, cut);
        }
    }

    private void delete((string, string) file)
    {

        if (Directory.Exists(file.Item1))
        {
            var unused = new Rm(new List<string>() { "-r", file.Item1 });    
        } 
        else
        {
            var unused = new Rm(new List<string>() { file.Item1 });
        }

        foreach (var entry in selected.Where(entry => entry.Value is true ))
        {
            if (Directory.Exists(entry.Key))
            {
                var unused = new Rm(new List<string>() { "-r", entry.Key });    
            } 
            else
            {
                var unused = new Rm(new List<string>() { entry.Key });
            }
        }
        
        getFilesAndTypes(currentPath);
    }

    private void paste()
    {
        foreach (var file in filesToCopyCut)
        {
            if (file.Value == false)
            {
                if (Directory.Exists(file.Key))
                {
                    var unused = new Cp(new List<string>()
                        { "-r", file.Key, currentPath + Path.GetFileName(file.Key) });
                }
                else
                {
                    var unused = new Cp(new List<string>() { file.Key, currentPath + Path.GetFileName(file.Key) });
                }
            }
            else
            {
                if (Directory.Exists(file.Key))
                {
                    var unused = new Mv(new List<string>() { "-r", file.Key, currentPath + Path.GetFileName(file.Key) });
                }
                else
                {
                    var unused = new Mv(new List<string>() { file.Key, currentPath + Path.GetFileName(file.Key) });
                }
            }

        }
                
        getFilesAndTypes(currentPath);
        filesToCopyCut.Clear();
    }

    // TODO make both of these idiot proof so you won't be able to create a file with the same name in directory
    private void newTextFile()
    {
        var unused = new MkFile(new List<string>() { currentPath + "NewTextFile.txt" } );
        
        getFilesAndTypes(currentPath);
    }
    
    private void newDirectory()
    {
        var unused = new MkDir(new List<string>() { currentPath + "NewDirectory" } );
        
        getFilesAndTypes(currentPath);
    }
    
    
    #endregion

    #endregion

    #region misc

    private void moveToDirectory(string path)
    {
        goingBackHistory.Clear();
        
        currentPath = path + Path.DirectorySeparatorChar;
        updateInputPath();
        
        getFilesAndTypes(currentPath);
    }

    private void initDoubleClickActions()
    {
        doubleClickActions = new Dictionary<string, Action<string>>()
        {
            {
                Consts.folderType, moveToDirectory
            },
            {
                "text/", Util.openFileInNewTextEditor
            },
        };
    }

    private void updateInputPath()
    {
        currentPathInputField = currentPath;
        currentPathInputField = currentPathInputField[filesystemPrefix.Length..];
    }
    
    #endregion
    
}