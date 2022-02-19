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
    
    public void loadFont(string path, int size)
    {
        throw new NotImplementedException("Doesn't work");
        
        ImFontPtr font = io.Fonts.AddFontFromFileTTF(path, size);
        ImGui.PushFont(font);
        renderer.RebuildFontAtlas();
    }
    
}