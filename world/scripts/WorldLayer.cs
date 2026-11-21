using Godot;

[GlobalClass]
public partial class WorldLayer : Resource
{
    [Export] public Tile ForegroundTile { get; set; }
    [Export] public Tile BackgroundTile { get; set; }
    [Export] public float Weight { get; set; } = 16;
}