using Godot;
using System;

/// <summary>
/// Singleton
/// A ideia é centralizar algumas ações que dependem do contexto
/// </summary>
public partial class SingletonInputManager : Node
{
    public static SingletonInputManager Instance { get; private set; }

    [Export]
    public bool PodeInteragirComputador = false;

    [Signal]
    public delegate void ShowUIComputerEventHandler();
    public override void _Ready()
    {
        Instance = this;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
		//todo pensar melhor nisso aqui se vai ser assim, nao terminei de pensar
		if (PodeInteragirComputador && @event.IsActionPressed("interagir")){
            EmitSignal(SignalName.ShowUIComputer);
        }
    }
}
