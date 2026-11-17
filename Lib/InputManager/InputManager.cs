using Godot;
using System;

/// <summary>
/// Singleton
/// A ideia é centralizar algumas ações que dependem do contexto
/// </summary>
public partial class InputManager : Node
{
    public override void _UnhandledInput(InputEvent @event)
    {
		//todo pensar melhor nisso aqui se vai ser assim, nao terminei de pensar
		if (@event.IsActionPressed("interagir")){
            Console.WriteLine("interagiu");
			//todo disparar um signal para mostrar a UI
        }
        base._UnhandledInput(@event);
    }
}
