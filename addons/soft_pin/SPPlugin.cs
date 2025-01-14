#if TOOLS
using Godot;
using System;

[Tool]
public partial class SPPlugin : EditorPlugin
{
    Script script;
    Texture2D icon;

    public override void _EnterTree()
    {
        // Initialization of the plugin goes here.
        script = ResourceLoader.Load<Script>("res://addons/soft_pin/SoftPin.cs");
        icon = ResourceLoader.Load<Texture2D>("res://addons/soft_pin/softPin Icon.svg");
        AddCustomType("SoftPin", "Node", script, icon);
    }

    public override void _ExitTree()
    {
        // Clean-up of the plugin goes here.
        RemoveCustomType("SoftPin");
    }
}
#endif
