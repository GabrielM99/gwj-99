using Godot;

public partial class WorldGenerator : Node2D
{
    [Export] public World World { get; private set; }
    [Export] public Tile RockFGTile { get; set; }
    [Export] public Tile RockBGTile { get; set; }
    [Export] public int Size { get; set; } = 64;
    [Export] public int CoreSize { get; set; } = 3;
    [Export(PropertyHint.Range, "0, 1")] public float Noise { get; set; } = 0f;

    public override void _Ready()
    {
        base._Ready();

        int halfSize = Size / 2;

        GD.Print("Gerando mundo...");

        for (int x = -halfSize; x <= halfSize; x++)
        {
            for (int y = -halfSize; y <= halfSize; y++)
            {
                if (GD.Randf() <= Noise)
                {
                    continue;
                }

                Vector2I position = new(x, y);

                float distance = position.DistanceTo(Vector2I.Zero);

                if (distance > CoreSize && distance <= halfSize)
                {
                    World.SetTile(position, RockFGTile, 1);

                    if (distance < halfSize - 2)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                World.SetTile(position + new Vector2I(i, j), RockBGTile, 0);
                            }
                        }
                    }
                }
            }
        }
    }
}
