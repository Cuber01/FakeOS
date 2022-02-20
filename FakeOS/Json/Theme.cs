using System.Collections.Generic;
using ImGuiNET;
using System.Numerics;
using Newtonsoft.Json;

namespace FakeOS.Json;

public class Theme
{
    public float? Alpha { get; set; } = null;                   // Global alpha applies to everything in Dear ImGui.
    public float?        DisabledAlpha { get; set; } = null;              // Additional alpha multiplier applied by BeginDisabled(). Multiply over current value of Alpha.
    public Vector2?      WindowPadding { get; set; } = null;              // Padding within a window.
    public float?        WindowRounding { get; set; } = null;             // Radius of window corners rounding. Set to 0.0f to have rectangular windows. Large values tend to lead to variety of artifacts and are not recommended.
    public float?        WindowBorderSize { get; set; } = null;           // Thickness of border around windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).
    public Vector2?     WindowMinSize { get; set; } = null;              // Minimum window size. This is a global setting. If you want to constraint individual windows, use SetNextWindowSizeConstraints().
    public Vector2?      WindowTitleAlign { get; set; } = null;           // Alignment for title bar text. Defaults to (0.0f,0.5f) for left-aligned,vertically centered.
    public ImGuiDir?     WindowMenuButtonPosition { get; set; } = null;   // Side of the collapsing/docking button in the title bar (None/Left/Right). Defaults to ImGuiDir_Left.
    public float?        ChildRounding { get; set; } = null;              // Radius of child window corners rounding. Set to 0.0f to have rectangular windows.
    public float?        ChildBorderSize { get; set; } = null;            // Thickness of border around child windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).
    public float?        PopupRounding { get; set; } = null;              // Radius of popup window corners rounding. (Note that tooltip windows use WindowRounding)
    public float?        PopupBorderSize { get; set; } = null;            // Thickness of border around popup/tooltip windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).
    public Vector2?      FramePadding { get; set; } = null;               // Padding within a framed rectangle (used by most widgets).
    public float?        FrameRounding { get; set; } = null;              // Radius of frame corners rounding. Set to 0.0f to have rectangular frame (used by most widgets).
    public float?        FrameBorderSize { get; set; } = null;            // Thickness of border around frames. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).
    public Vector2?      ItemSpacing { get; set; } = null;                // Horizontal and vertical spacing between widgets/lines.
    public Vector2?      ItemInnerSpacing { get; set; } = null;           // Horizontal and vertical spacing between within elements of a composed widget (e.g. a slider and its label).
    public Vector2?      CellPadding { get; set; } = null;                // Padding within a table cell
    public Vector2?      TouchExtraPadding { get; set; } = null;          // Expand reactive bounding box for touch-based system where touch position is not accurate enough. Unfortunately we don't sort widgets so priority on overlap will always be given to the first widget. So don't grow this too much!
    public float?        IndentSpacing { get; set; } = null;              // Horizontal indentation when e.g. entering a tree node. Generally == (FontSize + FramePadding.x*2).
    public float?        ColumnsMinSpacing { get; set; } = null;          // Minimum horizontal spacing between two columns. Preferably > (FramePadding.x + 1).
    public float?        ScrollbarSize { get; set; } = null;              // Width of the vertical scrollbar, Height of the horizontal scrollbar.
    public float?        ScrollbarRounding { get; set; } = null;          // Radius of grab corners for scrollbar.
    public float?        GrabMinSize { get; set; } = null;                // Minimum width/height of a grab box for slider/scrollbar.
    public float?        GrabRounding { get; set; } = null;               // Radius of grabs corners rounding. Set to 0.0f to have rectangular slider grabs.
    public float?        LogSliderDeadzone { get; set; } = null;          // The size in pixels of the dead-zone around zero on logarithmic sliders that cross zero.
    public float?        TabRounding { get; set; } = null;                // Radius of upper corners of a tab. Set to 0.0f to have rectangular tabs.
    public float?        TabBorderSize { get; set; } = null;              // Thickness of border around tabs.
    public float?        TabMinWidthForCloseButton { get; set; } = null;  // Minimum width for close button to appears on an unselected tab when hovered. Set to 0.0f to always show when hovering, set to FLT_MAX to never show close button unless selected.
    public ImGuiDir?     ColorButtonPosition { get; set; } = null;        // Side of the color button in the ColorEdit4 widget (left/right). Defaults to ImGuiDir_Right.
    public Vector2?      ButtonTextAlign { get; set; } = null;            // Alignment of button text when button is larger than text. Defaults to (0.5f, 0.5f) (centered).
    public Vector2?      SelectableTextAlign { get; set; } = null;        // Alignment of selectable text. Defaults to (0.0f, 0.0f) (top-left aligned). It's generally important to keep this left-aligned if you want to lay multiple items on a same line.
    public Vector2?      DisplayWindowPadding { get; set; } = null;       // Window position are clamped to be visible within the display area or monitors by at least this amount. Only applies to regular windows.
    public Vector2?      DisplaySafeAreaPadding { get; set; } = null;     // If you cannot see the edges of your screen (e.g. on a TV) increase the safe area padding. Apply to popups/tooltips as well regular windows. NB: Prefer configuring your TV sets correctly!
    public float?        MouseCursorScale { get; set; } = null;           // Scale software rendered mouse cursor (when io.MouseDrawCursor is enabled). May be removed later.
    public bool?         AntiAliasedLines { get; set; } = null;           // Enable anti-aliased lines/borders. Disable if you are really tight on CPU/GPU. Latched at the beginning of the frame (copied to ImDrawList).
    
    public bool?         AntiAliasedLinesUseTex { get; set; } = null;     // Enable anti-aliased lines/borders using textures where possible. Require backend to render with bilinear filtering. Latched at the beginning of the frame (copied to ImDrawList).
    public bool?         AntiAliasedFill { get; set; } = null;            // Enable anti-aliased edges around filled shapes (rounded rectangles, circles, etc.). Disable if you are really tight on CPU/GPU. Latched at the beginning of the frame (copied to ImDrawList).
    public float?        CurveTessellationTol { get; set; } = null;       // Tessellation tolerance when using PathBezierCurveTo() without a specific number of segments. Decrease for highly tessellated curves (higher quality, more polygons), increase to reduce quality.
    public float?        CircleTessellationMaxError { get; set; } = null; // Maximum error (in pixels) allowed when using AddCircle()/AddCircleFilled() or drawing rounded corner rectangles with no explicit segment count specified. Decrease for higher quality but more geometry.
    
    public Dictionary<string, List<float>> Colors { get; set; } = null;

} 