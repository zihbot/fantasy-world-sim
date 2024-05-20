using Godot;
using SimplexNoise;
using System;
using System.Linq;

public partial class TerrainMesh : MeshInstance3D
{
  private const int CHUNK_SIZE = 128;
  private const int SCALE_NUMBER = 4;
  public override void _EnterTree()
  {
    var start = DateTime.UtcNow;

    SimplexNoise.SimplexNoise.Seed = 1337;
    System.Collections.Generic.IEnumerable<float[,]> noises = Array.Empty<float[,]>();
    for (int i = 0; i < SCALE_NUMBER; i++)
    {
      SimplexNoise.SimplexNoise.Seed += 1;
      noises = noises.Append(SimplexNoise.SimplexNoise.Calc2D(CHUNK_SIZE + 1, CHUNK_SIZE + 1, 0.005f * MathF.Pow(2, i)));
    }
    var noise = new float[CHUNK_SIZE + 1, CHUNK_SIZE + 1];
    for (int i = 0; i < CHUNK_SIZE + 1; i++)
    {
      for (int j = 0; j < CHUNK_SIZE + 1; j++)
      {
        noise[i, j] = noises.Select((n, x) => n[i, j] / 1000 / MathF.Pow(2, x)).Sum();
      }
    }

    SurfaceTool st = new();
    st.Begin(Mesh.PrimitiveType.Triangles);

    for (int i = 0; i < CHUNK_SIZE; i++)
    {
      for (int j = 0; j < CHUNK_SIZE; j++)
      {
        st.SetColor(new Color((float)i / CHUNK_SIZE, (float)j / CHUNK_SIZE, 0));
        st.SetUV(new Vector2((float)i / CHUNK_SIZE, (float)j / CHUNK_SIZE));
        st.AddVertex(new Vector3((float)i / CHUNK_SIZE, noise[i, j], (float)j / CHUNK_SIZE));
        st.AddVertex(new Vector3((i + 1f) / CHUNK_SIZE, noise[i + 1, j], (float)j / CHUNK_SIZE));
        st.AddVertex(new Vector3((float)i / CHUNK_SIZE, noise[i, j + 1], (j + 1f) / CHUNK_SIZE));
        st.AddVertex(new Vector3((float)i / CHUNK_SIZE, noise[i, j + 1], (j + 1f) / CHUNK_SIZE));
        st.AddVertex(new Vector3((i + 1f) / CHUNK_SIZE, noise[i + 1, j], (float)j / CHUNK_SIZE));
        st.AddVertex(new Vector3((i + 1f) / CHUNK_SIZE, noise[i + 1, j + 1], (j + 1f) / CHUNK_SIZE));
      }
    }
    st.GenerateNormals();
    st.GenerateTangents();
    Mesh = st.Commit();

    // Print duration of the generation
    var end = DateTime.UtcNow;
    GD.Print("Duration: ", end - start);
  }

  public override void _Ready()
  {
  }
}
