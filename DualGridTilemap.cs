
using Godot;
using System.Collections.Generic;

[Tool]
public partial class DualGridTilemap : Node2D
{
    private enum TileType
    {
        None,
        Some
    }

    [Export] public int TileSize { get; set; } = 16;

    [Export] public TileMapLayer WorldMapLayer { get; set; }
    [Export] public TileMapLayer DisplayMapLayer { get; set; }

    private bool _isDirty;

    private readonly List<TileMapLayer> _worldLayers = [];
    private readonly Godot.Collections.Array<Tile> _tiles = [];
    private readonly Dictionary<(int Z, int SourceID), TileMapLayer> _displayLayers = [];
    private readonly Dictionary<int, Tile> _tilesBySourceID = [];

    private static readonly Vector2I[] Neighbours =
    [
        new(0, 0),
        new(1, 0),
        new(0, 1),
        new(1, 1)
    ];

    private static readonly Dictionary<(TileType, TileType, TileType, TileType), Vector2I> NeighboursToAtlasCoord = new()
    {
        { (TileType.Some, TileType.Some, TileType.Some, TileType.Some), new(2, 1) },
        { (TileType.None, TileType.None, TileType.None, TileType.Some), new(1, 3) },
        { (TileType.None, TileType.None, TileType.Some, TileType.None), new(0, 0) },
        { (TileType.None, TileType.Some, TileType.None, TileType.None), new(0, 2) },
        { (TileType.Some, TileType.None, TileType.None, TileType.None), new(3, 3) },
        { (TileType.None, TileType.Some, TileType.None, TileType.Some), new(1, 0) },
        { (TileType.Some, TileType.None, TileType.Some, TileType.None), new(3, 2) },
        { (TileType.None, TileType.None, TileType.Some, TileType.Some), new(3, 0) },
        { (TileType.Some, TileType.Some, TileType.None, TileType.None), new(1, 2) },
        { (TileType.None, TileType.Some, TileType.Some, TileType.Some), new(1, 1) },
        { (TileType.Some, TileType.None, TileType.Some, TileType.Some), new(2, 0) },
        { (TileType.Some, TileType.Some, TileType.None, TileType.Some), new(2, 2) },
        { (TileType.Some, TileType.Some, TileType.Some, TileType.None), new(3, 1) },
        { (TileType.None, TileType.Some, TileType.Some, TileType.None), new(2, 3) },
        { (TileType.Some, TileType.None, TileType.None, TileType.Some), new(0, 1) },
        { (TileType.None, TileType.None, TileType.None, TileType.None), new(-1, -1) }
    };

    public override void _Ready()
    {
        if (WorldMapLayer != null)
        {
            _worldLayers.Add(WorldMapLayer);
        }

        _isDirty = true;
    }

    public override void _Process(double delta)
    {
        if (!_isDirty)
        {
            return;
        }

        RefreshAllTiles();
        _isDirty = false;
    }

    public Vector2I LocalToMap(Vector2 position, int z = 0)
    {
        return EnsureWorldLayer(z).LocalToMap(position);
    }

    public void SetTile(Vector2I coords, Tile tile, int z = 0)
    {
        if (z < 0)
        {
            GD.PrintErr("Z cannot be negative.");
            return;
        }

        if (tile != null)
        {
            _tilesBySourceID[tile.SourceID] = tile;
        }

        TileMapLayer worldLayer = EnsureWorldLayer(z);

        if (tile == null)
        {
            worldLayer.EraseCell(coords);
        }
        else
        {
            worldLayer.SetCell(coords, tile.SourceID, Vector2I.Zero);
        }

        _isDirty = true;
    }

    private TileMapLayer EnsureWorldLayer(int z)
    {
        while (_worldLayers.Count <= z)
        {
            int currentZ = _worldLayers.Count;

            TileMapLayer layer;

            if (currentZ == 0)
            {
                layer = WorldMapLayer;
            }
            else
            {
                layer = DuplicateLayer(WorldMapLayer, $"World_Z{currentZ}");
            }

            layer.Clear();
            layer.ZIndex = currentZ * 100;

            _worldLayers.Add(layer);
        }

        return _worldLayers[z];
    }

    private TileMapLayer EnsureDisplayLayer(int z, Tile tile)
    {
        (int z, int SourceID) key = (z, tile.SourceID);

        if (_displayLayers.TryGetValue(key, out TileMapLayer existing))
        {
            return existing;
        }

        TileMapLayer layer;

        if (_displayLayers.Count == 0)
        {
            layer = DisplayMapLayer;
        }
        else
        {
            layer = DuplicateLayer(DisplayMapLayer, $"Display_Z{z}_Source{tile.SourceID}");
        }

        layer.Clear();
        layer.ZIndex = z * 100 + tile.SortingOrder;

        _displayLayers.Add(key, layer);

        return layer;
    }

    private TileMapLayer DuplicateLayer(TileMapLayer template, string layerName)
    {
        TileMapLayer duplicate = (TileMapLayer)template.Duplicate();
        duplicate.Name = layerName;

        AddChild(duplicate);

        duplicate.Transform = template.Transform;

        return duplicate;
    }

    public void RefreshAllTiles()
    {
        foreach (TileMapLayer layer in _displayLayers.Values)
        {
            layer.Clear();
        }

        for (int z = 0; z < _worldLayers.Count; z++)
        {
            TileMapLayer worldLayer = _worldLayers[z];

            foreach (Vector2I cellPos in worldLayer.GetUsedCells())
            {
                int sourceID = worldLayer.GetCellSourceId(cellPos);

                if (!_tilesBySourceID.TryGetValue(sourceID, out Tile tile))
                {
                    continue;
                }

                TileMapLayer displayLayer = EnsureDisplayLayer(z, tile);
                RefreshDisplayTile(worldLayer, displayLayer, cellPos, tile);
            }
        }
    }

    private void RefreshDisplayTile(TileMapLayer worldLayer, TileMapLayer displayLayer, Vector2I position, Tile tile)
    {
        foreach (Vector2I neighbour in Neighbours)
        {
            Vector2I displayPos = position + neighbour;
            Vector2I atlasCoords = CalculateDisplayTileAtlasCoords(worldLayer, displayPos, tile);

            if (atlasCoords == -Vector2I.One)
            {
                displayLayer.EraseCell(displayPos);
                continue;
            }

            displayLayer.SetCell(displayPos, tile.SourceID, atlasCoords);
        }
    }

    private Vector2I CalculateDisplayTileAtlasCoords(TileMapLayer worldLayer, Vector2I coords, Tile targetTile)
    {
        TileType botRight = GetMatchingTileType(worldLayer, coords - Neighbours[0], targetTile);
        TileType botLeft = GetMatchingTileType(worldLayer, coords - Neighbours[1], targetTile);
        TileType topRight = GetMatchingTileType(worldLayer, coords - Neighbours[2], targetTile);
        TileType topLeft = GetMatchingTileType(worldLayer, coords - Neighbours[3], targetTile);

        return NeighboursToAtlasCoord[(topLeft, topRight, botLeft, botRight)];
    }

    private TileType GetMatchingTileType(TileMapLayer worldLayer, Vector2I coords, Tile targetTile)
    {
        int sourceID = worldLayer.GetCellSourceId(coords);

        return sourceID == targetTile.SourceID
            ? TileType.Some
            : TileType.None;
    }
}