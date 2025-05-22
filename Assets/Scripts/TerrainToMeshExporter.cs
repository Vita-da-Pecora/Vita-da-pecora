using UnityEngine;
using System.IO;

public class TerrainToMeshExporter : MonoBehaviour
{
    public Terrain terrain;

    public void ExportTerrainMesh()
    {
        TerrainData data = terrain.terrainData;

        int w = data.heightmapResolution;
        int h = data.heightmapResolution;

        Vector3 meshScale = data.size;
        float[,] heights = data.GetHeights(0, 0, w, h);

        Vector3[] vertices = new Vector3[w * h];
        Vector2[] uv = new Vector2[w * h];
        int[] triangles = new int[(w - 1) * (h - 1) * 6];

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                int i = y * w + x;
                float height = heights[y, x];
                vertices[i] = new Vector3(x * meshScale.x / w, height * meshScale.y, y * meshScale.z / h);
                uv[i] = new Vector2((float)x / w, (float)y / h);
            }
        }

        int index = 0;
        for (int y = 0; y < h - 1; y++)
        {
            for (int x = 0; x < w - 1; x++)
            {
                int i = y * w + x;

                triangles[index++] = i;
                triangles[index++] = i + w;
                triangles[index++] = i + 1;

                triangles[index++] = i + 1;
                triangles[index++] = i + w;
                triangles[index++] = i + w + 1;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        GameObject terrainMesh = new GameObject("TerrainMesh", typeof(MeshFilter), typeof(MeshRenderer));
        terrainMesh.GetComponent<MeshFilter>().mesh = mesh;
        terrainMesh.transform.position = terrain.transform.position;
    }
}
