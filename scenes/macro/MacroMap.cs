using Godot;
using System;

public partial class MacroMap : Control
{
	public override void _Ready()
	{

	}

	public override void _Draw()
	{
		DrawPolygon(new Vector2[] {
			new(10, 0),
			new(10, Size.Y),
			new(Size.X, Size.Y),
			new(Size.X, 0),
		}, new Color[] { new(0, 0, 0, 0.5f) });
	}
}
