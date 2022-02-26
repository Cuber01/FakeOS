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
        
        getFilesAndTypes(pathToFilesystem);
    }

    public override void draw()
    {
        // if (!running) return;
        //
        // ImGui.Begin(name, ref running, windowFlags);
        //
        // ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(2, 2));
        //
        // showTable();
        //
        // ImGui.PopStyleVar();
        //
        // ImGui.End();
    }
    
    public override void update()
    {
        // if (!running) return;
        //
        // fileCheckTimer++;
        // if (fileCheckTimer == framesBetweenFileChecks)
        // {
        //     fileCheckTimer = 0;
        //     getFilesAndTypes();
        // }
    }

    private void getFilesAndTypes(string directory)
    {
        foreach (string file in Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories))
        {
            filesAndTypes.Add(file, MimeTypes.GetContentType(file));
            Console.WriteLine(file);
        }
    }

    
    
    private void showTable()
    {
        if (ImGui.BeginTable("#main", 1, tableFlags))
        {
            showFiles();
            
            ImGui.EndTable();
        }
    }

    private void showFiles()
    {
        int id = 0;
        
        // Use object uid as identifier. Most commonly you could also use the object pointer as a base ID.
        ImGui.PushID(id);
        
        // Text and Tree nodes are less high than framed widgets, using AlignTextToFramePadding() we add vertical spacing to make the tree lines equal high.
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);
        ImGui.AlignTextToFramePadding();
        
        bool nodeOpen = ImGui.TreeNode("#filesystem" + id, "Filesystem");

        if (nodeOpen)
        {
            
            for (int i = 0; i < filesAndTypes.Count; i++)
            {
                displayFile(filesAndTypes, i);
            }
            
        }
        
        ImGui.PopID();
        
    }

    private void displayFile(Dictionary<string, string> files,int i)
    {
        ImGui.PushID(i);

        // If it's a folder, we go further
        if (filesAndTypes.Values.ElementAt(i) == Consts.folderType)
        {
            string[] otherFiles = Directory.GetFiles(filesAndTypes.Keys.ElementAt(i));

            // If the folder is empty, just display it
            if (otherFiles.Length == 0)
            {
                ImGui.AlignTextToFramePadding();
                ImGui.TreeNodeEx("#file" + i, fileFlags, Path.GetFileName(filesAndTypes.Keys.ElementAt(i)));
            }

        }
        else
        {
            ImGui.AlignTextToFramePadding();
            ImGui.TreeNodeEx("#file" + i, fileFlags, Path.GetFileName(filesAndTypes.Keys.ElementAt(i)));
        }

        ImGui.PopID();
    }

//     for (int i = 0; i < 6; i++)
//     {
//         ImGui.PushID(i); // Use field index as identifier.
//         if (i < 2)
//         {
//             ImGui.AlignTextToFramePadding();
//             ImGui.TreeNodeEx("Field", fileFlags, AwesomeIcons.FolderO + " Folder" + i);
//         }
//         else
//         {
//             // Here we use a TreeNode to highlight on hover (we could use e.g. Selectable as well)
//             ImGui.TableNextRow();
//             ImGui.TableSetColumnIndex(0);
//             ImGui.AlignTextToFramePadding();
//                     
//             ImGui.TreeNodeEx("Field", fileFlags, AwesomeIcons.FileImageO + " Field_" + i);
//         }
//                 
//         ImGui.PopID();
//     }
//             
//     ImGui.TreePop();
// }
//         
// ImGui.PopID();
    
}