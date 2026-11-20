using Godot;
using System;

public readonly struct TileInfo(Tile tile, Vector2I position)
{
	public Tile Tile { get; } = tile;
	public Vector2I Position { get; } = position;
}