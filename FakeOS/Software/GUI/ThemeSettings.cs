using System;
using System.Collections.Generic;
using System.IO;
using FakeOS.General;
using ImGuiNET;

namespace FakeOS.Software.GUI;

public class ThemeSettings : GuiSoftware
{
    private readonly string[] themeFiles = Directory.GetFiles(Consts.themeLocation);
    private readonly string[] fontFiles = Directory.GetFiles(Consts.fontsLocation);

    private readonly StyleManager styleManager;

    public ThemeSettings(List<string> args, StyleManager styleManager, Action<string> echo = null, string executionDirectory = null) : base(args, echo, executionDirectory)
    {
        this.styleManager = styleManager;
    }

    private int currentTheme = 0;
    private int currentFont = 0;
    
    public override void imGuiUpdate()
    {
        base.imGuiUpdate();

        if (ImGui.BeginCombo("Current Theme", Path.GetFileNameWithoutExtension(themeFiles[currentTheme])))
        {
            for (int n = 0; n < themeFiles.Length; n++)
            {
                bool is_selected = (currentTheme == n);
                if (ImGui.Selectable(Path.GetFileNameWithoutExtension(themeFiles[n]), is_selected))
                {
                    styleManager.setTheme(Path.GetFileNameWithoutExtension(themeFiles[n]));
                    currentTheme = n;
                }
                    
                // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
                if (is_selected)
                {
                    ImGui.SetItemDefaultFocus();    
                }
            }

            ImGui.EndCombo();
        }
        
        if (ImGui.BeginCombo("Current Font", Path.GetFileNameWithoutExtension(fontFiles[currentFont])))
        {
            for (int n = 0; n < fontFiles.Length; n++)
            {
                bool is_selected = (currentFont == n);
                if (ImGui.Selectable(Path.GetFileNameWithoutExtension(fontFiles[n]), is_selected))
                {
                    ImFontPtr a = ImGui.GetIO().Fonts.AddFontFromFileTTF(fontFiles[n], Consts.startupFont.Item2);
                    ImGui.PushFont(a);
                    currentFont = n;
                    ImGui.PopFont();
                }
                    
                // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
                if (is_selected)
                {
                    ImGui.SetItemDefaultFocus();    
                }
            }

            ImGui.EndCombo();
        }

    }
}