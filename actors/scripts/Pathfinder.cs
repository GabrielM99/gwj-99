using Godot;

public partial class Pathfinder : Node
{
	[Export] public NPC NPC { get; set; }
	[Export] public float PathUpdateInterval { get; set; } = 0.5f;
	[Export] public float NextWaypointDistance { get; set; } = 8f;

	public Vector2 NextWaypoint => _path != null && _nextWaypointIndex >= 0 &&
		_nextWaypointIndex < _path.Length ? _path[_nextWaypointIndex] : NPC.GlobalPosition;
	public Vector2 NextWaypointDirection => (NextWaypoint - NPC.GlobalPosition).Normalized();

	private Vector2? _targetPosition;
	private double _pathUpdateTimer;
	private Vector2[] _path;
	private int _nextWaypointIndex;

	public override void _Process(double delta)
	{
		base._Process(delta);

		UpdatePath(delta);
		UpdateWaypoints();
	}

	public void SetTargetPosition(Vector2? position)
	{
		_targetPosition = position;

		if (position == null)
		{
			_pathUpdateTimer = 0f;
			_nextWaypointIndex = 0;
			_path = null;
		}
		else
		{
			_nextWaypointIndex = 1;
		}
	}

	private void UpdatePath(double delta)
	{
		if (_targetPosition == null)
		{
			return;
		}

		_pathUpdateTimer += delta;

		if (_pathUpdateTimer >= PathUpdateInterval)
		{
			_path = NPC.World.PathfindGrid.GetPath(NPC.GlobalPosition, _targetPosition.Value);
			_pathUpdateTimer = 0f;
		}
	}

	private void UpdateWaypoints()
	{
		if (_path == null)
		{
			return;
		}

		float waypointDistance = NPC.GlobalPosition.DistanceSquaredTo(NextWaypoint);

		if (waypointDistance <= NextWaypointDistance * NextWaypointDistance)
		{
			_nextWaypointIndex = Mathf.Clamp(_nextWaypointIndex + 1, 0, _path.Length);
		}
	}
}