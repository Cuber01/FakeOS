using System;
using System.Collections.Generic;
using System.IO;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using FakeOS.Json;
using FakeOS.Tools;
using ImGuiNET;
using Newtonsoft.Json;

namespace FakeOS.General;

public class StyleManager
{
    private Dictionary<string, Theme> themes = new Dictionary<string, Theme>();
    private string[] themeFiles;
    
    private ImGuiStylePtr style;
    
    public StyleManager(string pathToThemes, ImGuiStylePtr style)
    {
        
        themeFiles = Directory.GetFiles(pathToThemes);
        this.style = style;
        
        foreach (var file in themeFiles)
        {
            if(Path.GetExtension(file) != ".json") continue;
            
            themes.Add(Path.GetFileNameWithoutExtension(file), JsonConvert.DeserializeObject<Theme>(FileReader.getFileString(file)));
        }
    
    }
    
    public void setTheme(string name)
    {
        Theme theme = themes[name];
        
        style.Alpha = theme.Alpha ?? style.Alpha;
        style.DisabledAlpha = theme.DisabledAlpha ?? style.DisabledAlpha;
        style.WindowPadding = theme.WindowPadding ?? style.WindowPadding;
        style.WindowRounding = theme.WindowRounding ?? style.WindowRounding;
        style.WindowBorderSize = theme.WindowBorderSize ?? style.WindowBorderSize;
        style.WindowMinSize = theme.WindowMinSize ?? style.WindowMinSize;
        style.WindowTitleAlign = theme.WindowTitleAlign ?? style.WindowTitleAlign;
        style.WindowMenuButtonPosition = theme.WindowMenuButtonPosition ?? style.WindowMenuButtonPosition;
        style.ChildRounding = theme.ChildRounding ?? style.ChildRounding;
        style.ChildBorderSize = theme.ChildBorderSize ?? style.ChildBorderSize;
        style.PopupRounding = theme.PopupRounding ?? style.PopupRounding;
        style.PopupBorderSize = theme.PopupBorderSize ?? style.PopupBorderSize;
        style.FramePadding = theme.FramePadding ?? style.FramePadding;
        style.FrameRounding = theme.FrameRounding ?? style.FrameRounding;
        style.FrameBorderSize = theme.FrameBorderSize ?? style.FrameBorderSize;
        style.ItemSpacing = theme.ItemSpacing ?? style.ItemSpacing;
        style.ItemInnerSpacing = theme.ItemInnerSpacing ?? style.ItemInnerSpacing;
        style.CellPadding = theme.CellPadding ?? style.CellPadding;
        style.TouchExtraPadding = theme.TouchExtraPadding ?? style.TouchExtraPadding;
        style.IndentSpacing = theme.IndentSpacing ?? style.IndentSpacing;
        style.ColumnsMinSpacing = theme.ColumnsMinSpacing ?? style.ColumnsMinSpacing;
        style.ScrollbarSize = theme.ScrollbarSize ?? style.ScrollbarSize;
        style.ScrollbarRounding = theme.ScrollbarRounding ?? style.ScrollbarRounding;
        style.GrabMinSize = theme.GrabMinSize ?? style.GrabMinSize;
        style.GrabRounding = theme.GrabRounding ?? style.GrabRounding;
        style.LogSliderDeadzone = theme.LogSliderDeadzone ?? style.LogSliderDeadzone;
        style.TabRounding = theme.TabRounding ?? style.TabRounding;
        style.TabBorderSize = theme.TabBorderSize ?? style.TabBorderSize;
        style.TabMinWidthForCloseButton = theme.TabMinWidthForCloseButton ?? style.TabMinWidthForCloseButton;
        style.ColorButtonPosition = theme.ColorButtonPosition ?? style.ColorButtonPosition;
        style.ButtonTextAlign = theme.ButtonTextAlign ?? style.ButtonTextAlign;
        style.SelectableTextAlign = theme.SelectableTextAlign ?? style.SelectableTextAlign;
        style.DisplayWindowPadding = theme.DisplayWindowPadding ?? style.DisplayWindowPadding;
        style.DisplaySafeAreaPadding = theme.DisplaySafeAreaPadding ?? style.DisplaySafeAreaPadding;
        style.MouseCursorScale = theme.MouseCursorScale ?? style.MouseCursorScale;
        style.AntiAliasedLines = theme.AntiAliasedLines ?? style.AntiAliasedLines;
        style.AntiAliasedFill = theme.AntiAliasedFill ?? style.AntiAliasedFill;
        style.CurveTessellationTol = theme.CurveTessellationTol ?? style.CurveTessellationTol;
        style.CircleTessellationMaxError = theme.CircleTessellationMaxError ?? style.CircleTessellationMaxError;
        style.AntiAliasedLinesUseTex = theme.AntiAliasedLinesUseTex ?? style.AntiAliasedLinesUseTex;

        for(int i = 0; i < theme.Colors.Count; i++)
        {
            int colorIndex;

            try
            {
                // Get index from enum by name
                colorIndex = (int)(ImGuiCol)Enum.Parse(typeof(ImGuiCol), theme.Colors.Keys.ElementAt(i));
            } catch (ArgumentException)
            {
                // Not all colors have to be satisfied
                continue;
            }

            // Reassign a list to Vector4
            List<float> cv = theme.Colors.Values.ElementAt(i);
            Vector4 color = new Vector4(cv[0], cv[1], cv[2], cv[3]);

            // Set color
            style.Colors[colorIndex] = color;
        }
    }

}