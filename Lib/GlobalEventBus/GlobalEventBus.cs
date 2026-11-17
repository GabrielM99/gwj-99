using Godot;
using System;

public partial class GlobalEventBus : Node
{
    public static GlobalEventBus Instance { get; private set; }

	[Signal]
    public delegate void ResetCameraOffsetEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }
}
