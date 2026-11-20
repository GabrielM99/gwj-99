using Godot;

public partial class RobotAI : Node
{
	[Export] public NPC NPC { get; set; }
	[Export] public Pathfinder Pathfinder { get; set; }

	public override void _Ready()
	{
		Vector2 randomPosition = new(395.0f, -121.0f);
		Pathfinder.SetTargetPosition(randomPosition);
	}

	public override void _Process(double delta)
	{
		NPC.Direction = Pathfinder.NextWaypointDirection;
	}
}
