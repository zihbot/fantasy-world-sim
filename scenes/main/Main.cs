using Godot;
using System;

public partial class Main : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (OS.IsDebugBuild())
		{
			var townView = ResourceLoader.Load<PackedScene>(Resources.ViewTownPath);
			AddChild(townView.Instantiate());
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
