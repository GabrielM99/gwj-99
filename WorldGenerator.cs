using Godot;

public partial class WorldGenerator : Node
{
    [Export] public DualGridTilemap Tilemap { get; private set; }
    [Export] public int Size { get; private set; } = 64;

    public override void _Ready()
    {
        base._Ready();

        int halfSize = Size / 2;

        GD.Print("Gerando mundo...");

        for (int x = -halfSize; x < halfSize; x++)
        {
            for (int y = -halfSize; y < halfSize; y++)
            {
                Vector2I position = new(x, y);

                float distance = position.DistanceTo(Vector2I.Zero);

                if (distance > 3 && distance <= halfSize)
                {
                    Tilemap.SetTile(position, TerrainType.RockBG);

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                        }
                    }
                }
            }
        }
    }
}
