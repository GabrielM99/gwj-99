using Godot;

public partial class WorldPathfindGrid : Node
{
	[Export] public DualGridTilemap Tilemap { get; set; }

	// Região do mundo em coordenadas de células do TileMap.
	[Export] public Rect2I Region { get; set; } = new(-64, -64, 128, 128);

	private AStarGrid2D _astar;

	private AStarGrid2D GetAstar()
	{
		_astar ??= new AStarGrid2D
		{
			Region = Region,
			CellSize = new Vector2I(1, 1)
		};
		_astar.Update();

		return _astar;
	}

	public void SetWall(Vector2I cell)
	{
		GetAstar().SetPointSolid(cell);
	}

	public Vector2I[] GetPath(Vector2 from, Vector2 to)
	{
		Vector2I start = Tilemap.LocalToMap(Tilemap.ToLocal(from));
		Vector2I end = Tilemap.LocalToMap(Tilemap.ToLocal(to));

		return [.. GetAstar().GetIdPath(start, end)];
	}

	public Vector2 GetWorldPosition(Vector2I cell)
	{
		return Tilemap.ToGlobal(Tilemap.MapToLocal(cell));
	}

	public bool IsWalkable(Vector2I cell)
	{
		return GetAstar().IsInBoundsv(cell) && !GetAstar().IsPointSolid(cell);
	}
}