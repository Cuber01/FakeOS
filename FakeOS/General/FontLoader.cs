using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using ImGuiNET;

namespace FakeOS.General;

public class FontLoader
{
    private readonly Dictionary<string, ImFontPtr> fonts = new Dictionary<string, ImFontPtr>();
    private ImGuiIOPtr io;
    
    public FontLoader(string fontPath, ImGuiIOPtr io)
    {
        this.io = io;
        mergeFontAwesome();

        string[] fontFiles = Directory.GetFiles(fontPath);

        foreach (var file in fontFiles)
        {
            if(Path.GetExtension(file) != ".ttf") continue;
            
            fonts.Add(Path.GetFileNameWithoutExtension(file), io.Fonts.AddFontFromFileTTF(file, Consts.defaultFontSize));
            mergeFontAwesome();
        }
    }

    // TODO fontAwesome is 1 smaller than other fonts. Not sure if it's a good idea at this point in time but it looked off.
    private void mergeFontAwesome() => mergeIconFont(Consts.fontAwesomeLocation, Consts.defaultFontSize - 1, (AwesomeIcons.IconMin, AwesomeIcons.IconMax));
    
    private unsafe ImFontPtr mergeIconFont(string path, int size, (ushort, ushort) range)
    {
        ImFontConfigPtr configuration = ImGuiNative.ImFontConfig_ImFontConfig();

        configuration.MergeMode  = true;
        configuration.PixelSnapH = true;

        GCHandle rangeHandle = GCHandle.Alloc(new ushort[]
        {
            range.Item1,
            range.Item2,
            0
        }, GCHandleType.Pinned);

        try
        {
            return ImGui.GetIO().Fonts.AddFontFromFileTTF(path, size, configuration, rangeHandle.AddrOfPinnedObject());
        }
        finally
        {
            configuration.Destroy();

            if (rangeHandle.IsAllocated)
            {
                rangeHandle.Free();
            }
        }
    }
    
}