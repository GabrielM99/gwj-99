using Godot;
using System;

public partial class UiComputador : CanvasLayer
{

    [Export]
    public Button ButtonClose;

    [Export]
    public VBoxContainer ContainerHorizontal;

    /// <summary>
    /// Placeholder pra mostrar que a aba pode abrir conforme o tipo de computador passado
    /// Remover no futuro
    /// </summary>
    private Label labelPlaceholder = new();

    /// <summary>
    /// Pra evitar que coisas se repitam ao pressionar o botao de interagir novamente
    /// </summary>
    private bool _uiAberta = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        InputManager.Instance.ShowUIComputer += Open;
        ButtonClose.Pressed += Close;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    
    private void Open(ComputerType TipoComputadorParaAbrir){
        if (!_uiAberta){
            labelPlaceholder.Text = "placeholder " + TipoComputadorParaAbrir.ToString();
            ContainerHorizontal.AddChild(labelPlaceholder);
            _uiAberta = true;
            Show();
        }
    }

	private void Close(){
        if (_uiAberta){
            ContainerHorizontal.RemoveChild(labelPlaceholder);
            GlobalEventBus.Instance.EmitSignal(GlobalEventBus.SignalName.ResetCameraOffset);
            _uiAberta = false;
            Hide();
        }
        
    }
}
