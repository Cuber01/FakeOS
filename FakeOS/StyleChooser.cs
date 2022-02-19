using System.Numerics;
using ImGuiNET;

namespace FakeOS;

public static class StyleChooser 
{
    public static void darkTheme()
    {
        ImGuiStylePtr style = ImGui.GetStyle();
        style.WindowRounding = 5.3f;
        style.FrameRounding = 2.3f;
        style.ScrollbarRounding = 0;


        style.Colors[(int)ImGuiCol.Text]                  = new Vector4(0.90f, 0.90f, 0.90f, 0.90f);
        style.Colors[(int)ImGuiCol.TextDisabled]          = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
        style.Colors[(int)ImGuiCol.WindowBg]              = new Vector4(0.09f, 0.09f, 0.15f, 1.00f);
        style.Colors[(int)ImGuiCol.PopupBg]               = new Vector4(0.05f, 0.05f, 0.10f, 0.85f);
        style.Colors[(int)ImGuiCol.Border]                = new Vector4(0.70f, 0.70f, 0.70f, 0.65f);
        style.Colors[(int)ImGuiCol.BorderShadow]          = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
        style.Colors[(int)ImGuiCol.FrameBg]               = new Vector4(0.00f, 0.00f, 0.01f, 1.00f);
        style.Colors[(int)ImGuiCol.FrameBgHovered]        = new Vector4(0.90f, 0.80f, 0.80f, 0.40f);
        style.Colors[(int)ImGuiCol.FrameBgActive]         = new Vector4(0.90f, 0.65f, 0.65f, 0.45f);
        style.Colors[(int)ImGuiCol.TitleBg]               = new Vector4(0.00f, 0.00f, 0.00f, 0.83f);
        style.Colors[(int)ImGuiCol.TitleBgCollapsed]      = new Vector4(0.40f, 0.40f, 0.80f, 0.20f);
        style.Colors[(int)ImGuiCol.TitleBgActive]         = new Vector4(0.00f, 0.00f, 0.00f, 0.87f);
        style.Colors[(int)ImGuiCol.MenuBarBg]             = new Vector4(0.01f, 0.01f, 0.02f, 0.80f);
        style.Colors[(int)ImGuiCol.ScrollbarBg]           = new Vector4(0.20f, 0.25f, 0.30f, 0.60f);
        style.Colors[(int)ImGuiCol.ScrollbarGrab]         = new Vector4(0.55f, 0.53f, 0.55f, 0.51f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered]  = new Vector4(0.56f, 0.56f, 0.56f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive]   = new Vector4(0.56f, 0.56f, 0.56f, 0.91f);
        style.Colors[(int)ImGuiCol.CheckMark]             = new Vector4(0.90f, 0.90f, 0.90f, 0.83f);
        style.Colors[(int)ImGuiCol.SliderGrab]            = new Vector4(0.70f, 0.70f, 0.70f, 0.62f);
        style.Colors[(int)ImGuiCol.SliderGrabActive]      = new Vector4(0.30f, 0.30f, 0.30f, 0.84f);
        style.Colors[(int)ImGuiCol.Button]                = new Vector4(0.48f, 0.72f, 0.89f, 0.49f);
        style.Colors[(int)ImGuiCol.ButtonHovered]         = new Vector4(0.50f, 0.69f, 0.99f, 0.68f);
        style.Colors[(int)ImGuiCol.ButtonActive]          = new Vector4(0.80f, 0.50f, 0.50f, 1.00f);
        style.Colors[(int)ImGuiCol.Header]                = new Vector4(0.30f, 0.69f, 1.00f, 0.53f);
        style.Colors[(int)ImGuiCol.HeaderHovered]         = new Vector4(0.44f, 0.61f, 0.86f, 1.00f);
        style.Colors[(int)ImGuiCol.HeaderActive]          = new Vector4(0.38f, 0.62f, 0.83f, 1.00f);
        style.Colors[(int)ImGuiCol.ResizeGrip]            = new Vector4(1.00f, 1.00f, 1.00f, 0.85f);
        style.Colors[(int)ImGuiCol.ResizeGripHovered]     = new Vector4(1.00f, 1.00f, 1.00f, 0.60f);
        style.Colors[(int)ImGuiCol.ResizeGripActive]      = new Vector4(1.00f, 1.00f, 1.00f, 0.90f);
        style.Colors[(int)ImGuiCol.PlotLines]             = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
        style.Colors[(int)ImGuiCol.PlotLinesHovered]      = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
        style.Colors[(int)ImGuiCol.PlotHistogram]         = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
        style.Colors[(int)ImGuiCol.PlotHistogramHovered]  = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);
        style.Colors[(int)ImGuiCol.TextSelectedBg]        = new Vector4(0.00f, 0.00f, 1.00f, 0.35f);
    }
}