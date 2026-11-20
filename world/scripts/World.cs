using Godot;

public partial class World : Node
{
	[Export] private DualGridTilemap Tilemap { get; set; }
	[Export] private WorldPathfindGrid PathfindGrid { get; set; }

	public void SetTile(Vector2I position, Tile tile, int z = 0)
	{
		Tilemap.SetTile(position, tile, z);

		if (z > 0)
		{
			PathfindGrid.SetWall(position);
		}
	}
}