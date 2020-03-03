using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private NativeArray<float> _densities;

    public NativeArray<float> Densities { get => _densities; set => _densities = value; }

    public float isolevel;

    public int chunkSize;

    private void Start()
    {
        _densities = new NativeArray<float>((chunkSize + 1) * (chunkSize + 1) * (chunkSize + 1), Allocator.Temp);
        for (int y = 0; y < chunkSize + 1; y++)
        {
            for (int x = 0; x < chunkSize + 1; x++)
            {
                int index = y * (chunkSize + 1) + x;
                float density = (noise.snoise(new float2(x, y)) + 1) * 0.5f;
                _densities[index] = density;
            }
        }

        GetComponent<MeshFilter>().mesh = MarchingSquares.CreateMesh(_densities, chunkSize, isolevel);
        _densities.Dispose();
    }
}
