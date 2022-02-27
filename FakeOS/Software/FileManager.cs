using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using FakeOS.Tools;
using ImGuiNET;

namespace FakeOS.Software;

public class FileManager : GuiSoftware
{
    private Dictionary<string, (string, string)> filesAndTypes = new Dictionary<string, (string, string)>();
    private Dictionary<string, Action<string>> doubleClickActions;
    
    private string currentPath;

    private const int framesBetweenFileChecks = 500;
    private int fileCheckTimer = 0;

    private const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.MenuBar;
    private const ImGuiTableFlags tableFlags = ImGuiTableFlags.BordersOuter | ImGuiTableFlags.Resizable;

    public FileManager(string path)
    {
        name = "File Manager";
        this.currentPath = path;

        initDoubleClickActions();
        getFilesAndTypes(currentPath);
    }

    #region mainUpdateLoops

    public override void imGuiUpdate()
    {
        if (!running) return;

        ImGui.Begin(name, ref running, windowFlags);

        ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(2, 2));
        
        
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

        bool nodeOpen = ImGui.TreeNodeEx("Filesystem", ImGuiTreeNodeFlags.DefaultOpen);

        if (nodeOpen)
        {
            
            for (int i = 0; i < filesAndTypes.Count; i++)
            {
                var fileKey = filesAndTypes.Keys.ElementAt(i);
                var fileVal = filesAndTypes.Values.ElementAt(i);
                
                if (fileKey.Contains(currentPath))
                {
                    handleFile((fileKey, fileVal.Item1, fileVal.Item2), i);
                }
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
    
    #region buttons
    
    #endregion

    #region misc

    private void moveToDirectory(string path)
    {
        currentPath = path;
        
        fileCheckTimer = 0;
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
    
    #endregion
    
}