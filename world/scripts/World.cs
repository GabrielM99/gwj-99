using Godot;

public partial class World : Node
{
	[Export] private DualGridTilemap Tilemap { get; set; }
	[Export] public WorldPathfindGrid PathfindGrid { get; private set; }

	// TODO: Temporário.
	[Export] private NPC Robot { get; set; }

	public override void _Ready()
	{
		base._Ready();
		AttachEntity(Robot);
	}

	public void SetTile(Vector2I position, Tile tile, int z = 0)
	{
		Tilemap.SetTile(position, tile, z);

		if (z > 0)
		{
			PathfindGrid.SetWall(position);
		}
	}

	public void SpawnEntity(PackedScene entityScene)
	{
		Node instance = entityScene.Instantiate();

		if (instance is not WorldEntity entity)
		{
			GD.PushError($"A cena precisa ter um nó do tipo {nameof(WorldEntity)}.");
			instance.Free();
			return;
		}

		AttachEntity(entity);
		AddChild(entity);
	}

	public void AttachEntity(WorldEntity entity)
	{
		entity.World = this;
	}
}