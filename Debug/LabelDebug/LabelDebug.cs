using Godot;

public partial class LabelDebug : Label
{
	public override void _Ready()
	{
		//se destroi se nao for debug, serve só pra identificar os objetos com arte feia de placeholder
		if(!OS.IsDebugBuild()){
            this.QueueFree();
        }
	}
}
