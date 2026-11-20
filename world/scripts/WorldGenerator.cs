using Godot;

public partial class WorldGenerator : Node2D
{
    [Export] public bool Enabled { get; private set; } = true;
    [Export] public World World { get; private set; }
    [Export] public Tile RockFGTile { get; set; }
    [Export] public Tile RockBGTile { get; set; }
    [Export] public int Size { get; set; } = 64;
    [Export] public Vector2 CoreSize { get; set; } = new(16, 16);
    [Export] public Vector2I Center { get; set; }
    [Export(PropertyHint.Range, "0, 1")] public float Noise { get; set; } = 0f;

    public override void _Ready()
    {
        base._Ready();

        if (!Enabled)
        {
            return;
        }

        int halfSize = Size / 2;

        GD.Print("Gerando mundo...");

        for (int x = -halfSize + Center.X; x <= halfSize + Center.X; x++)
        {
            for (int y = -halfSize + Center.Y; y <= halfSize + Center.Y; y++)
            {
                if (GD.Randf() <= Noise)
                {
                    continue;
                }

                Vector2I position = new(x, y);

                float distance = position.DistanceTo(Center);

                if (!IsInsideCapsule(position - Center, CoreSize) && distance <= halfSize)
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

    private bool IsInsideCapsule(Vector2 position, Vector2 size)
    {
        float radius = Mathf.Min(size.X, size.Y) / 2f;

        Vector2 halfSegment = new(
            size.X > size.Y ? (size.X - size.Y) / 2f : 0f,
            size.Y > size.X ? (size.Y - size.X) / 2f : 0f
        );

        Vector2 closestPoint = new(
            Mathf.Clamp(position.X, -halfSegment.X, halfSegment.X),
            Mathf.Clamp(position.Y, -halfSegment.Y, halfSegment.Y)
        );

        return position.DistanceSquaredTo(closestPoint) <= radius * radius;
    }
}
