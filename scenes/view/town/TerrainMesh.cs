using Godot;
using SimplexNoise;
using System;

public partial class TerrainMesh : MeshInstance3D
{
  private const int CHUNK_SIZE = 128;
  public override void _EnterTree()
  {
    SimplexNoise.SimplexNoise.Seed = 1337;
    float[,] noise = SimplexNoise.SimplexNoise.Calc2D(CHUNK_SIZE + 1, CHUNK_SIZE + 1, 0.01f);
    SurfaceTool st = new();
    st.Begin(Mesh.PrimitiveType.Triangles);

    for (int i = 0; i < CHUNK_SIZE; i++)
    {
      for (int j = 0; j < CHUNK_SIZE; j++)
      {
        st.SetColor(new Color((float)i / CHUNK_SIZE, (float)j / CHUNK_SIZE, 0));
        st.SetUV(new Vector2((float)i / CHUNK_SIZE, (float)j / CHUNK_SIZE));
        st.AddVertex(new Vector3((float)i / CHUNK_SIZE, noise[i, j] / 1000, (float)j / CHUNK_SIZE));
        st.AddVertex(new Vector3((i + 1f) / CHUNK_SIZE, noise[i + 1, j] / 1000, (float)j / CHUNK_SIZE));
        st.AddVertex(new Vector3((float)i / CHUNK_SIZE, noise[i, j + 1] / 1000, (j + 1f) / CHUNK_SIZE));
        st.AddVertex(new Vector3((float)i / CHUNK_SIZE, noise[i, j + 1] / 1000, (j + 1f) / CHUNK_SIZE));
        st.AddVertex(new Vector3((i + 1f) / CHUNK_SIZE, noise[i + 1, j] / 1000, (float)j / CHUNK_SIZE));
        st.AddVertex(new Vector3((i + 1f) / CHUNK_SIZE, noise[i + 1, j + 1] / 1000, (j + 1f) / CHUNK_SIZE));
      }
    }
    st.GenerateNormals();
    st.GenerateTangents();
    Mesh = st.Commit();
  }

  public override void _Ready()
  {
  }
}
