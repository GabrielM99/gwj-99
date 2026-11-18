
using Godot;

[GlobalClass]
public partial class Tile : Resource
{
    [Export] public int SourceID { get; set; } = 1;

    // Relative rendering order between tile types at the same Z.
    [Export] public int SortingOrder { get; set; } = 0;
}