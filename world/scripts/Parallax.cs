using System;
using Godot;

public partial class Parallax : Node2D
{
	[Export] public Sprite2D Sprite { get; private set; }
	[Export] public Godot.Camera2D Camera { get; private set; }
	[Export] public float Speed { get; private set; } = 1f;

	public override void _Process(double delta)
	{
		base._Process(delta);

		Rect2 spriteRect = Sprite.RegionRect;
		spriteRect.Position = Camera.GlobalPosition * Speed * 0.5f;

		Sprite.RegionRect = spriteRect;
		Sprite.Scale = new Vector2(1f / Camera.Zoom.X, 1f / Camera.Zoom.Y);
	}
}
