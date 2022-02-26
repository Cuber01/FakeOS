using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using FakeOS.General;
using FakeOS.Tools;
using ImGuiNET;

namespace FakeOS.Software;

public class FileManager : GuiSoftware
{
    private Dictionary<string, string> filesAndTypes = new Dictionary<string, string>();
    private readonly string pathToFilesystem;

    private const int framesBetweenFileChecks = 60;
    private int fileCheckTimer = 0;
    
    private const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.MenuBar;
    private const ImGuiTableFlags tableFlags = ImGuiTableFlags.BordersOuter | ImGuiTableFlags.Resizable;

    private const ImGuiTreeNodeFlags folderFlags = ImGuiTreeNodeFlags.Leaf | ImGuiTreeNodeFlags.NoTreePushOnOpen | ImGuiTreeNodeFlags.Bullet;
    private const ImGuiTreeNodeFlags fileFlags = ImGuiTreeNodeFlags.Leaf | ImGuiTreeNodeFlags.NoTreePushOnOpen;

    public FileManager(string pathToFilesystem)
    {
        name = "File Manager";
        this.pathToFilesystem = pathToFilesystem;
    }

    public override void draw()
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
            getFilesAndTypes();
        }
    }

    private void getFilesAndTypes()
    {
        string[] files = Directory.GetFiles(pathToFilesystem);

        for(int i = 0; i <= files.Length; i++)
        {
            filesAndTypes.Add(files[i], MimeTypes.GetContentType(files[i]));
        }
    }
    
    private void showTable()
    {

        if (ImGui.BeginTable("split", 1, tableFlags))
        {
            showFile("Filesystem", 1);
            
            ImGui.EndTable();
        }
    }

    private void showFilesystem(string path)
    {
        string[] files = Directory.GetFiles(path);

        for(int i = 0; i <= files.Length; i++)
        {
            showFile(files[i], i);
        }
    }

    private void showFile(string file, int id)
    {
        // if (MimeTypes.GetContentType(file) == "application/octet-stream")
        // {
        //     string[] otherFiles = Directory.GetFiles(file);
        //
        //     if (otherFiles.Length == 0) continue;
        // }
        //
        
        // Use object uid as identifier. Most commonly you could also use the object pointer as a base ID.
        ImGui.PushID(id);
        
        // Text and Tree nodes are less high than framed widgets, using AlignTextToFramePadding() we add vertical spacing to make the tree lines equal high.
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.AlignTextToFramePadding();
        
        bool nodeOpen = ImGui.TreeNode("#file" + id, file);

        if (nodeOpen)
        {
            for (int i = 0; i < 6; i++)
            {
                ImGui.PushID(i); // Use field index as identifier.
                if (i < 2)
                {
                    ImGui.AlignTextToFramePadding();
                    ImGui.TreeNodeEx("Field", fileFlags, AwesomeIcons.FolderO + " Folder" + i);
                }
                else
                {
                    // Here we use a TreeNode to highlight on hover (we could use e.g. Selectable as well)
                    ImGui.TableNextRow();
                    ImGui.TableSetColumnIndex(0);
                    ImGui.AlignTextToFramePadding();
                    
                    ImGui.TreeNodeEx("Field", fileFlags, AwesomeIcons.FileImageO + " Field_" + i);
                }
                
                ImGui.PopID();
            }
            
            ImGui.TreePop();
        }
        
        ImGui.PopID();
    }
    
}