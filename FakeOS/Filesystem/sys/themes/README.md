# How to install new themes:

1. Get a string containing C++ code with styles.

Example:

```
style.WindowRounding = 5.3f;
style.FrameRounding = 2.3f;
style.ScrollbarRounding = 0;

style.Colors[ImGuiCol_Text]                  = ImVec4(0.90f, 0.90f, 0.90f, 0.90f);
style.Colors[ImGuiCol_TextDisabled]          = ImVec4(0.60f, 0.60f, 0.60f, 1.00f);
style.Colors[ImGuiCol_WindowBg]              = ImVec4(0.09f, 0.09f, 0.15f, 1.00f);
style.Colors[ImGuiCol_ChildWindowBg]         = ImVec4(0.00f, 0.00f, 0.00f, 0.00f);
style.Colors[ImGuiCol_PopupBg]               = ImVec4(0.05f, 0.05f, 0.10f, 0.85f);
style.Colors[ImGuiCol_Border]                = ImVec4(0.70f, 0.70f, 0.70f, 0.65f);
style.Colors[ImGuiCol_BorderShadow]          = ImVec4(0.00f, 0.00f, 0.00f, 0.00f);
```

2. Go to /FakeOS/FakeOS/tools (repo: https://github.com/Cuber01/FakeOS)

3. Edit the $input variable inside cpp_to_json.rb, and paste your theme there

4. Run ``ruby cpp_to_json.rb``

5. Copy the output and paste it to a new .json file at sys/themes

6. You'll have to do a bit of work manually, sorry. Use other themes files for reference.

The color values should be enclosed in "Colors": {}

You need to edit non-color values to include " at the end.

# Credits

retroDark - https://github.com/ocornut/imgui/issues/707#issuecomment-252413954
lightGreen - https://github.com/ocornut/imgui/pull/1776
charcoalGray - https://github.com/ocornut/imgui/issues/707#issuecomment-463758243
yetAnotherDark - https://github.com/ocornut/imgui/issues/707#issuecomment-512669512
