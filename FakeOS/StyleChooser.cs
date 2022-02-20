using System.Numerics;
using ImGuiNET;

namespace FakeOS;

public static class StyleChooser 
{
    // https://github.com/ocornut/imgui/issues/707#issuecomment-252413954
    public static void codzDark()
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

    // https://github.com/ocornut/imgui/pull/1776
    public static void lightGreen()
    {
        ImGuiStylePtr style = ImGui.GetStyle();

        style.WindowRounding    = 2.0f;             // Radius of window corners rounding. Set to 0.0f to have rectangular windows
        style.ScrollbarRounding = 3.0f;             // Radius of grab corners rounding for scrollbar
        style.GrabRounding      = 2.0f;             // Radius of grabs corners rounding. Set to 0.0f to have rectangular slider grabs.
        style.AntiAliasedLines  = true;
        style.AntiAliasedFill   = true;
        style.WindowRounding    = 2;
        style.ChildRounding     = 2;
        style.ScrollbarSize     = 16;
        style.ScrollbarRounding = 3;
        style.GrabRounding      = 2;
        style.ItemSpacing.X     = 10;
        style.ItemSpacing.Y     = 4;
        style.IndentSpacing     = 22;
        style.FramePadding.X    = 6;
        style.FramePadding.Y    = 4;
        style.Alpha             = 1.0f;
        style.FrameRounding     = 3.0f;
    
        style.Colors[(int)ImGuiCol.Text]                  = new Vector4(0.00f, 0.00f, 0.00f, 1.00f);
        style.Colors[(int)ImGuiCol.TextDisabled]          = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
        style.Colors[(int)ImGuiCol.WindowBg]              = new Vector4(0.86f, 0.86f, 0.86f, 1.00f);
        style.Colors[(int)ImGuiCol.ChildBg]               = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
        style.Colors[(int)ImGuiCol.PopupBg]               = new Vector4(0.93f, 0.93f, 0.93f, 0.98f);
        style.Colors[(int)ImGuiCol.Border]                = new Vector4(0.71f, 0.71f, 0.71f, 0.08f);
        style.Colors[(int)ImGuiCol.BorderShadow]          = new Vector4(0.00f, 0.00f, 0.00f, 0.04f);
        style.Colors[(int)ImGuiCol.FrameBg]               = new Vector4(0.71f, 0.71f, 0.71f, 0.55f);
        style.Colors[(int)ImGuiCol.FrameBgHovered]        = new Vector4(0.94f, 0.94f, 0.94f, 0.55f);
        style.Colors[(int)ImGuiCol.FrameBgActive]         = new Vector4(0.71f, 0.78f, 0.69f, 0.98f);
        style.Colors[(int)ImGuiCol.TitleBg]               = new Vector4(0.85f, 0.85f, 0.85f, 1.00f);
        style.Colors[(int)ImGuiCol.TitleBgCollapsed]      = new Vector4(0.82f, 0.78f, 0.78f, 0.51f);
        style.Colors[(int)ImGuiCol.TitleBgActive]         = new Vector4(0.78f, 0.78f, 0.78f, 1.00f);
        style.Colors[(int)ImGuiCol.MenuBarBg]             = new Vector4(0.86f, 0.86f, 0.86f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarBg]           = new Vector4(0.20f, 0.25f, 0.30f, 0.61f);
        style.Colors[(int)ImGuiCol.ScrollbarGrab]         = new Vector4(0.90f, 0.90f, 0.90f, 0.30f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered]  = new Vector4(0.92f, 0.92f, 0.92f, 0.78f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive]   = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
        style.Colors[(int)ImGuiCol.CheckMark]             = new Vector4(0.184f, 0.407f, 0.193f, 1.00f);
        style.Colors[(int)ImGuiCol.SliderGrab]            = new Vector4(0.26f, 0.59f, 0.98f, 0.78f);
        style.Colors[(int)ImGuiCol.SliderGrabActive]      = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
        style.Colors[(int)ImGuiCol.Button]                = new Vector4(0.71f, 0.78f, 0.69f, 0.40f);
        style.Colors[(int)ImGuiCol.ButtonHovered]         = new Vector4(0.725f, 0.805f, 0.702f, 1.00f);
        style.Colors[(int)ImGuiCol.ButtonActive]          = new Vector4(0.793f, 0.900f, 0.836f, 1.00f);
        style.Colors[(int)ImGuiCol.Header]                = new Vector4(0.71f, 0.78f, 0.69f, 0.31f);
        style.Colors[(int)ImGuiCol.HeaderHovered]         = new Vector4(0.71f, 0.78f, 0.69f, 0.80f);
        style.Colors[(int)ImGuiCol.HeaderActive]          = new Vector4(0.71f, 0.78f, 0.69f, 1.00f);
        style.Colors[(int)ImGuiCol.Separator]              =new Vector4(0.39f, 0.39f, 0.39f, 1.00f);
        style.Colors[(int)ImGuiCol.SeparatorHovered]       =new Vector4(0.14f, 0.44f, 0.80f, 0.78f);
        style.Colors[(int)ImGuiCol.SeparatorActive]        =new Vector4(0.14f, 0.44f, 0.80f, 1.00f);
        style.Colors[(int)ImGuiCol.ResizeGrip]            = new Vector4(1.00f, 1.00f, 1.00f, 0.00f);
        style.Colors[(int)ImGuiCol.ResizeGripHovered]     = new Vector4(0.26f, 0.59f, 0.98f, 0.45f);
        style.Colors[(int)ImGuiCol.ResizeGripActive]      = new Vector4(0.26f, 0.59f, 0.98f, 0.78f);
        style.Colors[(int)ImGuiCol.PlotLines]             = new Vector4(0.39f, 0.39f, 0.39f, 1.00f);
        style.Colors[(int)ImGuiCol.PlotLinesHovered]      = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
        style.Colors[(int)ImGuiCol.PlotHistogram]         = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
        style.Colors[(int)ImGuiCol.PlotHistogramHovered]  = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);
        style.Colors[(int)ImGuiCol.TextSelectedBg]        = new Vector4(0.26f, 0.59f, 0.98f, 0.35f);
        style.Colors[(int)ImGuiCol.ModalWindowDimBg]      = new Vector4(0.20f, 0.20f, 0.20f, 0.35f);
        style.Colors[(int)ImGuiCol.DragDropTarget]        = new Vector4(0.26f, 0.59f, 0.98f, 0.95f);
        style.Colors[(int)ImGuiCol.NavHighlight]          = style.Colors[(int)ImGuiCol.HeaderHovered];
        style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(0.70f, 0.70f, 0.70f, 0.70f);
    }

    public static void charcoalGray()
    {
        ImGuiStylePtr style = ImGui.GetStyle();

        style.Colors[(int)ImGuiCol.Text]                   = new Vector4(1.000f, 1.000f, 1.000f, 1.000f);
		style.Colors[(int)ImGuiCol.TextDisabled]           = new Vector4(0.500f, 0.500f, 0.500f, 1.000f);
		style.Colors[(int)ImGuiCol.WindowBg]               = new Vector4(0.180f, 0.180f, 0.180f, 1.000f);
		style.Colors[(int)ImGuiCol.ChildBg]                = new Vector4(0.280f, 0.280f, 0.280f, 0.000f);
		style.Colors[(int)ImGuiCol.PopupBg]                = new Vector4(0.313f, 0.313f, 0.313f, 1.000f);
		style.Colors[(int)ImGuiCol.Border]                 = new Vector4(0.266f, 0.266f, 0.266f, 1.000f);
		style.Colors[(int)ImGuiCol.BorderShadow]           = new Vector4(0.000f, 0.000f, 0.000f, 0.000f);
		style.Colors[(int)ImGuiCol.FrameBg]                = new Vector4(0.160f, 0.160f, 0.160f, 1.000f);
		style.Colors[(int)ImGuiCol.FrameBgHovered]         = new Vector4(0.200f, 0.200f, 0.200f, 1.000f);
		style.Colors[(int)ImGuiCol.FrameBgActive]          = new Vector4(0.280f, 0.280f, 0.280f, 1.000f);
		style.Colors[(int)ImGuiCol.TitleBg]                = new Vector4(0.148f, 0.148f, 0.148f, 1.000f);
		style.Colors[(int)ImGuiCol.TitleBgActive]          = new Vector4(0.148f, 0.148f, 0.148f, 1.000f);
		style.Colors[(int)ImGuiCol.TitleBgCollapsed]       = new Vector4(0.148f, 0.148f, 0.148f, 1.000f);
		style.Colors[(int)ImGuiCol.MenuBarBg]              = new Vector4(0.195f, 0.195f, 0.195f, 1.000f);
		style.Colors[(int)ImGuiCol.ScrollbarBg]            = new Vector4(0.160f, 0.160f, 0.160f, 1.000f);
		style.Colors[(int)ImGuiCol.ScrollbarGrab]          = new Vector4(0.277f, 0.277f, 0.277f, 1.000f);
		style.Colors[(int)ImGuiCol.ScrollbarGrabHovered]   = new Vector4(0.300f, 0.300f, 0.300f, 1.000f);
		style.Colors[(int)ImGuiCol.ScrollbarGrabActive]    = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
		style.Colors[(int)ImGuiCol.CheckMark]              = new Vector4(1.000f, 1.000f, 1.000f, 1.000f);
		style.Colors[(int)ImGuiCol.SliderGrab]             = new Vector4(0.391f, 0.391f, 0.391f, 1.000f);
		style.Colors[(int)ImGuiCol.SliderGrabActive]       = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
		style.Colors[(int)ImGuiCol.Button]                 = new Vector4(1.000f, 1.000f, 1.000f, 0.000f);
		style.Colors[(int)ImGuiCol.ButtonHovered]          = new Vector4(1.000f, 1.000f, 1.000f, 0.156f);
		style.Colors[(int)ImGuiCol.ButtonActive]           = new Vector4(1.000f, 1.000f, 1.000f, 0.391f);
		style.Colors[(int)ImGuiCol.Header]                 = new Vector4(0.313f, 0.313f, 0.313f, 1.000f);
		style.Colors[(int)ImGuiCol.HeaderHovered]          = new Vector4(0.469f, 0.469f, 0.469f, 1.000f);
		style.Colors[(int)ImGuiCol.HeaderActive]           = new Vector4(0.469f, 0.469f, 0.469f, 1.000f);
		style.Colors[(int)ImGuiCol.Separator]              = style.Colors[(int)ImGuiCol.Border];
		style.Colors[(int)ImGuiCol.SeparatorHovered]       = new Vector4(0.391f, 0.391f, 0.391f, 1.000f);
		style.Colors[(int)ImGuiCol.SeparatorActive]        = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
		style.Colors[(int)ImGuiCol.ResizeGrip]             = new Vector4(1.000f, 1.000f, 1.000f, 0.250f);
		style.Colors[(int)ImGuiCol.ResizeGripHovered]      = new Vector4(1.000f, 1.000f, 1.000f, 0.670f);
		style.Colors[(int)ImGuiCol.ResizeGripActive]       = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
		style.Colors[(int)ImGuiCol.Tab]                    = new Vector4(0.098f, 0.098f, 0.098f, 1.000f);
		style.Colors[(int)ImGuiCol.TabHovered]             = new Vector4(0.352f, 0.352f, 0.352f, 1.000f);
		style.Colors[(int)ImGuiCol.TabActive]              = new Vector4(0.195f, 0.195f, 0.195f, 1.000f);
		style.Colors[(int)ImGuiCol.TabUnfocused]           = new Vector4(0.098f, 0.098f, 0.098f, 1.000f);
		style.Colors[(int)ImGuiCol.TabUnfocusedActive]     = new Vector4(0.195f, 0.195f, 0.195f, 1.000f);
		style.Colors[(int)ImGuiCol.DockingPreview]         = new Vector4(1.000f, 0.391f, 0.000f, 0.781f);
		style.Colors[(int)ImGuiCol.DockingEmptyBg]         = new Vector4(0.180f, 0.180f, 0.180f, 1.000f);
		style.Colors[(int)ImGuiCol.PlotLines]              = new Vector4(0.469f, 0.469f, 0.469f, 1.000f);
		style.Colors[(int)ImGuiCol.PlotLinesHovered]       = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
		style.Colors[(int)ImGuiCol.PlotHistogram]          = new Vector4(0.586f, 0.586f, 0.586f, 1.000f);
		style.Colors[(int)ImGuiCol.PlotHistogramHovered]   = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
		style.Colors[(int)ImGuiCol.TextSelectedBg]         = new Vector4(1.000f, 1.000f, 1.000f, 0.156f);
		style.Colors[(int)ImGuiCol.DragDropTarget]         = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
		style.Colors[(int)ImGuiCol.NavHighlight]           = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
		style.Colors[(int)ImGuiCol.NavWindowingHighlight]  = new Vector4(1.000f, 0.391f, 0.000f, 1.000f);
		style.Colors[(int)ImGuiCol.NavWindowingDimBg]      = new Vector4(0.000f, 0.000f, 0.000f, 0.586f);
		style.Colors[(int)ImGuiCol.ModalWindowDimBg]       = new Vector4(0.000f, 0.000f, 0.000f, 0.586f);

		style.ChildRounding = 4.0f;
		style.FrameBorderSize = 1.0f;
		style.FrameRounding = 2.0f;
		style.GrabMinSize = 7.0f;
		style.PopupRounding = 2.0f;
		style.ScrollbarRounding = 12.0f;
		style.ScrollbarSize = 13.0f;
		style.TabBorderSize = 1.0f;
		style.TabRounding = 0.0f;
		style.WindowRounding = 4.0f;
    }

    public static void yetAnotherDark()
    {
	        //imGuiIO.Fonts->AddFontFromFileTTF("../data/Fonts/Ruda-Bold.ttf", 15.0f, &config); TODO
	        
    	ImGuiStylePtr style = ImGui.GetStyle();
        
        style.FrameRounding = 4.0f;
    	style.GrabRounding = 4.0f;

    	style.Colors[(int)ImGuiCol.Text] = new Vector4(0.95f, 0.96f, 0.98f, 1.00f);
    	style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.36f, 0.42f, 0.47f, 1.00f);
    	style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.11f, 0.15f, 0.17f, 1.00f);
    	style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.15f, 0.18f, 0.22f, 1.00f);
    	style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.08f, 0.08f, 0.08f, 0.94f);
    	style.Colors[(int)ImGuiCol.Border] = new Vector4(0.08f, 0.10f, 0.12f, 1.00f);
    	style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
    	style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
    	style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.12f, 0.20f, 0.28f, 1.00f);
    	style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.09f, 0.12f, 0.14f, 1.00f);
    	style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.09f, 0.12f, 0.14f, 0.65f);
    	style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.08f, 0.10f, 0.12f, 1.00f);
    	style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.00f, 0.00f, 0.00f, 0.51f);
    	style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.15f, 0.18f, 0.22f, 1.00f);
    	style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.02f, 0.02f, 0.02f, 0.39f);
    	style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
    	style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.18f, 0.22f, 0.25f, 1.00f);
    	style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.09f, 0.21f, 0.31f, 1.00f);
    	style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.28f, 0.56f, 1.00f, 1.00f);
    	style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.28f, 0.56f, 1.00f, 1.00f);
    	style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.37f, 0.61f, 1.00f, 1.00f);
    	style.Colors[(int)ImGuiCol.Button] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
    	style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.28f, 0.56f, 1.00f, 1.00f);
    	style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.06f, 0.53f, 0.98f, 1.00f);
    	style.Colors[(int)ImGuiCol.Header] = new Vector4(0.20f, 0.25f, 0.29f, 0.55f);
    	style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.80f);
    	style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
    	style.Colors[(int)ImGuiCol.Separator] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
    	style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.10f, 0.40f, 0.75f, 0.78f);
    	style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.10f, 0.40f, 0.75f, 1.00f);
    	style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.26f, 0.59f, 0.98f, 0.25f);
    	style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.67f);
    	style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.95f);
    	style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.11f, 0.15f, 0.17f, 1.00f);
    	style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.80f);
    	style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.20f, 0.25f, 0.29f, 1.00f);
    	style.Colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.11f, 0.15f, 0.17f, 1.00f);
    	style.Colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.11f, 0.15f, 0.17f, 1.00f);
    	style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.61f, 0.61f, 0.61f, 1.00f);
    	style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
    	style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
    	style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);
    	style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.26f, 0.59f, 0.98f, 0.35f);
    	style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.00f, 1.00f, 0.00f, 0.90f);
    	style.Colors[(int)ImGuiCol.NavHighlight] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
    	style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.00f, 1.00f, 1.00f, 0.70f);
    	style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.20f);
    	style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.35f);

		}
    }