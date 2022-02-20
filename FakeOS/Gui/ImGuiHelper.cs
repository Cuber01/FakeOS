using System;
using System.Runtime.InteropServices;
using ImGuiNET;

namespace FakeOS.Gui;

public class ImGuiHelper
{
    private ImGuiRenderer renderer;
    private ImGuiIOPtr io;
    
    public ImGuiHelper(ImGuiRenderer renderer, ImGuiIOPtr io)
    {
        this.renderer = renderer;
        this.io = io;
    }

    public void initializeDefaultSettings()
    {
        
    }
    
    public void loadFont(string path, int size)
    {
       io.Fonts.AddFontFromFileTTF(path, size);
        //renderer.RebuildFontAtlas();
    }
    
}