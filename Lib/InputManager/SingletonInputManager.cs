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
    [Export]
    public TipoComputadorEnum TipoComputadorParaAbrir = TipoComputadorEnum.nenhum;


    [Signal]
    public delegate void ShowUIComputerEventHandler(TipoComputadorEnum TipoComputadorParaAbrir);
    /// <summary>
    /// Para ser utilizado junto do ShowUIComputer
    /// </summary>
    [Signal]
    public delegate void ApplyCameraOffsetEventHandler();
    public override void _Ready()
    {
        Instance = this;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
		if (PodeInteragirComputador && @event.IsActionPressed("interagir")){
            EmitSignal(SignalName.ShowUIComputer,Variant.From(TipoComputadorParaAbrir));
            EmitSignal(SignalName.ApplyCameraOffset);
        }
    }
}
