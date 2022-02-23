using System;
using System.Collections.Generic;
using System.IO;
using ImGuiNET;

namespace FakeOS.General;

public class FontLoader
{
    private Dictionary<string, ImFontPtr> fonts = new Dictionary<string, ImFontPtr>();
    private ImGuiIOPtr io;
    
    public FontLoader(string fontPath, ImGuiIOPtr io)
    {
        this.io = io;
        
        string[] fontFiles = Directory.GetFiles(fontPath);

        fonts.Add("ImGuiDefault", io.Fonts.AddFontDefault());
        
        foreach (var file in fontFiles)
        {
            if(Path.GetExtension(file) != ".ttf") continue;
            
            fonts.Add(Path.GetFileNameWithoutExtension(file), io.Fonts.AddFontFromFileTTF(file, Consts.defaultFontSize));
        }
    }

    public void mergeFontAwesome(string path)
    {
        ImFontConfigPtr config = new ImFontConfigPtr();
        config.MergeMode = true;
        config.GlyphMinAdvanceX = Consts.defaultFontSize;

        //IntPtr iconRanges = new IntPtr() { FontAwesome.IconMin, FontAwesome.IconMax, 0 };
        //ImWchar icon_ranges[] = { ICON_MIN_FA, ICON_MAX_FA, 0 };
        io.Fonts.AddFontFromFileTTF(path, Consts.defaultFontSize, config);
    }
}