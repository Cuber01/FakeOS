using System.Collections.Generic;
using System.IO;
using System.Numerics;
using FakeOS.Json;
using ImGuiNET;
using Newtonsoft.Json;

namespace FakeOS;

public class StyleManager
{
    private List<Theme> themes = new List<Theme>();
    private string[] themeFiles; 
    
    public StyleManager(string pathToThemes)
    {
        themeFiles = Directory.GetFiles(pathToThemes);

        foreach (var file in themeFiles)
        {
            if(Path.GetExtension(file) != ".json") continue;
            
            themes.Add(JsonConvert.DeserializeObject<Theme>(FileReader.getFileString(file)));
        }

    }
    
    public static void retroDark()
    {
        
    }

}