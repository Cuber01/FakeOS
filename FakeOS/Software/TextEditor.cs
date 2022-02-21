using System;
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
    
    private string text = "";
    private const ImGuiInputTextFlags multilineTextFlags = ImGuiInputTextFlags.CallbackCharFilter | ImGuiInputTextFlags.AllowTabInput;
    private const ImGuiWindowFlags windowFlags = ImGuiWindowFlags.Modal | ImGuiWindowFlags.MenuBar;

    public TextEditor()
    {
        this.name = "Text Editor";
        running = true;
    }
    

    public override void draw()
    {
        if (!running) return;

        ImGui.Begin(name, ref running, windowFlags);

        ImGui.InputTextMultiline("", ref text, UInt16.MaxValue, new Vector2(1000, 1000),
            multilineTextFlags);

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
                //Do something
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
            
            if(ImGui.MenuItem("Quit", "Alt + F4"))
            {
                //Do something
            }
            
            ImGui.EndMenu();
        }

        
        ImGui.EndMenuBar();
        
        //
        // ImGui.MenuItem("(demo menu)", null, false, false);
        // if (ImGui.MenuItem("New")) {}
        // if (ImGui.MenuItem("Open", "Ctrl+O")) {}
        // if (ImGui.BeginMenu("Open Recent"))
        // {
        //     ImGui.MenuItem("fish_hat.c");
        //     ImGui.MenuItem("fish_hat.inl");
        //     ImGui.MenuItem("fish_hat.h");
        //     if (ImGui.BeginMenu("More.."))
        //     {
        //         ImGui.MenuItem("Hello");
        //         ImGui.MenuItem("Sailor");
        //         if (ImGui.BeginMenu("Recurse.."))
        //         {
        //             ImGui.EndMenu();
        //         }
        //         ImGui.EndMenu();
        //     }
        //     ImGui.EndMenu();
        // }
        // if (ImGui.MenuItem("Save", "Ctrl+S")) {}
        // if (ImGui.MenuItem("Save As..")) {}

    }
    
    
}