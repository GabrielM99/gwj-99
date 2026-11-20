using Godot;
using System;

public partial class Projectile : Node2D
{

    [Export]
    public int Dano { get; set; } = 1;

	[Export]
    public Area2D AreaDeteccao;

    [Export]
    public float Velocidade = 200;

    public Node2D Alvo { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        AreaDeteccao.AreaEntered += OnContato;
    }

	/// <summary>
    /// O projetil persegue o alvo
    /// </summary>
    /// <param name="delta"></param>
	public override void _PhysicsProcess(double delta)
    {
        if (!IsInstanceValid(Alvo))
        {
            QueueFree();
            return;
        }
        var dir = (Alvo.GlobalPosition - GlobalPosition).Normalized();
        GlobalPosition += dir * Velocidade * (float)delta;
    }

	private void OnContato(Node2D node){
		if (node.GetParent() is NPC npc && npc.TipoNpc == NPCType.Enemy){
            npc.vida.AplicarDano(Dano);
        }
        QueueFree();
    }
}
