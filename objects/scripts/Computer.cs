using Godot;
using System;
using System.Security.AccessControl;

public partial class Computer : Node2D
{
	/// <summary>
    /// Define que UI o computador que esta interagindo vai abrir
    /// </summary>
    [Export]
    public ComputerType TipoComputador = ComputerType.None;

    [Export]
    public Area2D areaDeteccao;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        areaDeteccao.BodyEntered += OnBodyEntered;
        areaDeteccao.BodyExited += OnBodyExited;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node2D body){
        if (body is Player player){
            //aqui identifica que o jogador esta no range do pc e pode interagir
            InputManager.Instance.PodeInteragirComputador = true;
            InputManager.Instance.TipoComputadorParaAbrir = TipoComputador;
        }
	}


	private void OnBodyExited(Node2D body){
		if (body is Player player){
			//aqui identifica que ele saiu e nao esta mais na frente do pc
            InputManager.Instance.PodeInteragirComputador = false;
            InputManager.Instance.TipoComputadorParaAbrir = ComputerType.None;
        }
	}
}
