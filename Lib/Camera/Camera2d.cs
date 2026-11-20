using Godot;
using System;

public partial class Camera2d : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        SingletonInputManager.Instance.ApplyCameraOffset += ApplyOffset;
        GlobalEventBus.Instance.ResetCameraOffset += ResetOffset;
    }

	private void ApplyOffset(){
		// se apertar o botao de interacao varias vezes nao vai ficar acumulando offset
		if (Offset == Vector2.Zero){
        	Offset += new Vector2(100,0);
		}
    }

	private void ResetOffset(){
        Offset = Vector2.Zero;
    }
}
