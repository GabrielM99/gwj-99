using Godot;
using System;

public partial class Ladder : Node2D
{

    [Export]
    public Area2D AreaDeteccao;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        AreaDeteccao.BodyEntered += OnBodyEntered;
        AreaDeteccao.BodyExited += OnBodyExited;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node2D body){
		if (body is Player player){
            player.CanUseLadder = true;
        }
	}


	private void OnBodyExited(Node2D body){
		if (body is Player player){
            player.CanUseLadder = false;
        }
	}
}
