using Godot;
using System;

public partial class Torre : Node2D
{

    [Export]
    public Area2D RangeDeteccao;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        RangeDeteccao.AreaEntered += InimigoDetectado;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void InimigoDetectado(Node2D area){
		if (area.GetParent() is Npc npc && npc.TipoNpc == TipoNpcEnum.inimigo){
            GD.Print("ataca");
        }
	}
}
