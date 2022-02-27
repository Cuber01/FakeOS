using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using FakeOS.General;
using FakeOS.Tools;
using ImGuiNET;

namespace FakeOS.Software;

public class FileManager : GuiSoftware
{
    private Dictionary<string, (string, string)> filesAndTypes = new Dictionary<string, (string, string)>();
    private Dictionary<string, Action<string>> doubleClickActions;
    private List<string> goingBackHistory = new List<string>();

    private string currentPathInputField;
    private string currentPath;

    private const int framesBetweenFileChecks = 500;
    private int fileCheckTimer = 0;

    private const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.MenuBar;
    private const ImGuiTableFlags tableFlags = ImGuiTableFlags.BordersOuter | ImGuiTableFlags.Resizable | ImGuiTableFlags.Sortable;

    private readonly string filesystemPrefix = $".{Path.DirectorySeparatorChar}Filesystem";

    public FileManager(string path)
    {
        name = "File Manager";
        
        this.currentPath = path;
        
        updateInputPath();

        initDoubleClickActions();
        getFilesAndTypes(currentPath);
    }

    #region mainUpdateLoops

    public override void imGuiUpdate()
    {
        if (!running) return;

        ImGui.Begin(name, ref running, windowFlags);

        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(2, 2));
        
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

        ImGui.AlignTextToFramePadding();
        ImGui.Selectable(file.Item3 + ' ' + Path.GetFileName(file.Item1));

        if (ImGui.IsItemHovered() && ImGui.IsMouseDoubleClicked(0))
        {
            handleDoubleClick((file.Item1, file.Item2));
        }

        ImGui.PopID();
    }

    // Item1 is path, Item2 is type
    private void handleDoubleClick((string, string) file)
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

        foreach (string file in Directory.GetFileSystemEntries(directory))
        {
            filesAndTypes.Add(file, MimeTypes.GetContentType(file));
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
            data =>
            {
                if (ImGui.IsKeyDown(ImGuiKey.Enter))
                {
                    // Submit the path on enter press
                    useTypedPath();
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

    private void useTypedPath()
    {
        currentPathInputField = Util.removeAfterCharacter(currentPathInputField, Path.DirectorySeparatorChar);

        // If the directory exists, we can safely move there
        if (Directory.Exists(filesystemPrefix + currentPathInputField))
        {
            currentPath = currentPathInputField;
            currentPath = currentPath.Insert(0, filesystemPrefix);
            
            getFilesAndTypes(currentPath);
        }
    }
    
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