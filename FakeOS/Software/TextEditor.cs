using System;
using System.Collections.Generic;
using System.Numerics;
using ImGuiNET;

namespace FakeOS.Software;

public class TextEditor : GuiSoftware
{
    /*  menu at the top of the screen
    
        if(ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                if(ImGui.MenuItem("New"))
                {
                    //Do something
                }
                ImGui.EndMenu();
            }
        
            ImGui.EndMainMenuBar();
        }
    
    */

    private const string defaultDocName = "Unsaved Document";
        
    // ImGui can't handle 2 tabs with the same name properly
    private int defaultDocNameCount = 0;

    private TextEditorTabs tabBar = new TextEditorTabs();

    private const ImGuiTabBarFlags tabBarFlags = ImGuiTabBarFlags.Reorderable | 
                                                 ImGuiTabBarFlags.FittingPolicyDefault |
                                                 ImGuiTabBarFlags.AutoSelectNewTabs |
                                                 ImGuiTabBarFlags.NoCloseWithMiddleMouseButton;
    
    private const ImGuiInputTextFlags multilineTextFlags = ImGuiInputTextFlags.AllowTabInput;
    private const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.MenuBar;

    public TextEditor()
    {
        this.name = "Text Editor";
        this.tabBar.tabs.Add(new TextEditorTabs.ImGuiTab(defaultDocName));

        defaultDocNameCount++;
        
        running = true;
    }
    

    public override void draw()
    {
        if (!running) return;

        ImGui.Begin(name, ref running, windowFlags);

        ImGui.BeginTabBar("#main", tabBarFlags);

        foreach (var tab in tabBar.tabs)
        {
            tab.show();
        }
        
        ImGui.EndTabBar();

        menuBar();

        ImGui.End();

    }

    private void menuBar()
    {
        ImGui.BeginMenuBar();
        
        if (ImGui.BeginMenu("Menu"))
        {
            if(ImGui.MenuItem("New"))
            {
                tabBar.tabs.Add(new TextEditorTabs.ImGuiTab(defaultDocName + ' ' + defaultDocNameCount));
                defaultDocNameCount++;
            }
            
            if(ImGui.MenuItem("Open", "Ctrl + O"))
            {
                //Do something
            }
            
            ImGui.Separator();
            
            if(ImGui.MenuItem("Save", "Ctrl + S"))
            {
                //Do something
            }
            
            if(ImGui.MenuItem("Save as"))
            {
                //Do something
            }

            ImGui.Separator();
            
            if(ImGui.MenuItem("Options"))
            {
                //Do something
            }
            
            if(ImGui.MenuItem("Help"))
            {
                //Do something
            }
            
            ImGui.Separator();
            
            if(ImGui.MenuItem("Quit", "Alt + F4"))
            {
                //Do something
            }
            
            ImGui.EndMenu();
        }

        
        ImGui.EndMenuBar();
    }

    public class TextEditorTabs
    {
        public List<ImGuiTab> tabs = new List<ImGuiTab>();

        public class ImGuiTab
        {
            private readonly string name;
            private string text = "";
        
            public ImGuiTab(string name)
            {
                this.name = name;
            }

            public void show()
            {
                
                if (ImGui.BeginTabItem(name))
                {
                    ImGui.InputTextMultiline("", ref text, UInt16.MaxValue, new Vector2(1000, 1000),
                        multilineTextFlags);
                    
                    ImGui.EndTabItem();
                }
                
            }
        
        }
    }
    
    
}