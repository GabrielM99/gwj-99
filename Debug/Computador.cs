using Godot;
using System;
using System.Security.AccessControl;

public partial class Computador : Node2D
{
	/// <summary>
    /// Define que UI o computador que esta interagindo vai abrir
    /// </summary>
    [Export]
    public TipoComputadorEnum TipoComputador = TipoComputadorEnum.nenhum;

    [Export]
    public Area2D areaDeteccao;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node2D body){
		if (body is Player player){
            //aqui identifica que o jogador esta no range do pc e pode interagir
        }
	}


	private void OnBodyExited(Node2D body){
		if (body is Player player){
			//aqui identifica que ele saiu e nao esta mais na frente do pc
        }
	}
}
