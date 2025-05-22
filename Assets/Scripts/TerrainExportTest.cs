using UnityEngine;

public class TerrainExportTest : MonoBehaviour
{
    void Start()
    {
        GetComponent<TerrainToMeshExporter>().ExportTerrainMesh();
    }
}
