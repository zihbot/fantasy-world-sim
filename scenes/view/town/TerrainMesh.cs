using Godot;
using SimplexNoise;
using System;

public partial class TerrainMesh : MeshInstance3D
{
    public override void _EnterTree()
    {
		SimplexNoise.SimplexNoise.Seed = 1337;
    }

    public override void _Ready()
	{
	}
}
