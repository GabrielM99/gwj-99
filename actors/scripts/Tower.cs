using Godot;
using System;

public partial class Tower : Node2D
{

    [Export]
    public Area2D RangeDeteccao;

    /// <summary>
    /// A torre pode passar o dano do projetil
    /// </summary>
    [Export]
    public int dano = 1;

    private static readonly PackedScene Projetil = GD.Load<PackedScene>("res://projetil/projetil.tscn");
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        RangeDeteccao.AreaEntered += InimigoDetectado;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    /// <summary>
    /// A area detectada aqui é o inimigo alvo
    /// </summary>
    /// <param name="area"></param>
	private void InimigoDetectado(Node2D area){
		if (area.GetParent() is NPC npc && npc.TipoNpc == NPCType.Enemy){
            var projetilInstancia = Projetil.Instantiate() as Projectile;
            projetilInstancia.Dano = dano;
            GetTree().CurrentScene.AddChild(projetilInstancia);
            projetilInstancia.GlobalPosition = this.GlobalPosition; //instanciado da mesma posicao que a torre
        }
	}
}
