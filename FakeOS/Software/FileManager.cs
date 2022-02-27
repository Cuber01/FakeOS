using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using FakeOS.Tools;
using ImGuiNET;

namespace FakeOS.Software;

public class FileManager : GuiSoftware
{
    private Dictionary<string, (string, string)> filesAndTypes = new Dictionary<string, (string, string)>();
    private string currentPath;

    private const int framesBetweenFileChecks = 1000;
    private int fileCheckTimer = 0;

    private const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.MenuBar;
    private const ImGuiTableFlags tableFlags = ImGuiTableFlags.BordersOuter | ImGuiTableFlags.Resizable;

    public FileManager(string path)
    {
        name = "File Manager";
        this.currentPath = path;

        getFilesAndTypes(currentPath);
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
            getFilesAndTypes(currentPath);
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
            int i = 0;
            foreach (var file in filesAndTypes)
            {
                if (file.Key.Contains(currentPath))
                {
                    displayFile((file.Key, file.Value.Item1, file.Value.Item2), i);
                }

                i++;
            }

        }

        ImGui.PopID();

    }

    // Item1 is path, Item2 is type, Item3 is icon
    private void displayFile((string, string, string) file, int id)
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

    private static void moveToDirectory(string path)
    {

    }

    private Dictionary<string, Action<string>> doubleClickActions = new Dictionary<string, Action<string>>()
    {
        {
            Consts.folderType, moveToDirectory
        },
        {
            "text/", Util.openFileInNewTextEditor
        },
    };



}