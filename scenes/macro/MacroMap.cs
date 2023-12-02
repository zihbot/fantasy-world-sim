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
			new(10, 10),
			new(10, Size.Y-10),
			new(Size.X-10, Size.Y-10),
			new(Size.X-10, 10),
		}, new Color[] { new(0, 0, 0, 0.5f) });
	}
}
