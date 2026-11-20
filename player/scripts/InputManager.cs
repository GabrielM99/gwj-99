using Godot;
using System;

/// <summary>
/// Singleton
/// A ideia é centralizar algumas ações que dependem do contexto
/// </summary>
public partial class InputManager : Node
{
    public static InputManager Instance { get; private set; }

    [Export]
    public bool PodeInteragirComputador = false;
    [Export]
    public ComputerType TipoComputadorParaAbrir = ComputerType.None;


    [Signal]
    public delegate void ShowUIComputerEventHandler(ComputerType TipoComputadorParaAbrir);
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
