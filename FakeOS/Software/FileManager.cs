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
    private const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.MenuBar;
    private const ImGuiTableFlags tableFlags = ImGuiTableFlags.BordersOuter | ImGuiTableFlags.Resizable;

    private const ImGuiTreeNodeFlags folderFlags = ImGuiTreeNodeFlags.Leaf | ImGuiTreeNodeFlags.NoTreePushOnOpen | ImGuiTreeNodeFlags.Bullet;
    private const ImGuiTreeNodeFlags fileFlags = ImGuiTreeNodeFlags.Leaf | ImGuiTreeNodeFlags.NoTreePushOnOpen;

    public FileManager()
    {
        name = "File Manager";
        
        showFilesystem("/home/cubeq/");
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

    private void showTable()
    {

        if (ImGui.BeginTable("split", 2, tableFlags))
        {

            for (int obj_i = 0; obj_i < 4; obj_i++)
            {
                showFile("Object", obj_i);
            }
            
            ImGui.EndTable();
        }
    }

    private void showFilesystem(string path)
    {
        string[] files = Directory.GetFiles(path);

        foreach (var file in files)
        {
            Console.WriteLine(MimeTypes.GetContentType(file));
        }
    }

    private void showFile(string filename, int id)
    {
        // Use object uid as identifier. Most commonly you could also use the object pointer as a base ID.
        ImGui.PushID(id);
        
        // Text and Tree nodes are less high than framed widgets, using AlignTextToFramePadding() we add vertical spacing to make the tree lines equal high.
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.AlignTextToFramePadding();
        
        bool nodeOpen = ImGui.TreeNode("#file" + id, filename);

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

    public override void update()
    {
        base.update();
    }
}