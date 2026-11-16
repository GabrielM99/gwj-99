using Godot;

public partial class LabelDebug : Label
{
	public override void _Ready()
	{
		if(!OS.IsDebugBuild()){
            this.QueueFree();
        }
	}
}
