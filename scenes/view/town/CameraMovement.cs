using Godot;
using System;

public partial class CameraMovement : Camera3D
{
	const float MOVE_SPEED = 0.05f;
	public override void _UnhandledKeyInput(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_up"))
		{
			GlobalTranslate(new Vector3(0, 0, -MOVE_SPEED));
		}
		else if (@event.IsActionPressed("ui_down"))
		{
			GlobalTranslate(new Vector3(0, 0, MOVE_SPEED));
		}
		else if (@event.IsActionPressed("ui_left"))
		{
			GlobalTranslate(new Vector3(-MOVE_SPEED, 0, 0));
		}
		else if (@event.IsActionPressed("ui_right"))
		{
			GlobalTranslate(new Vector3(MOVE_SPEED, 0, 0));
		}
	}
}
