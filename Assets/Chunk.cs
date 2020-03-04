using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private NativeArray<float> _densities;

    public NativeArray<float> Densities { get => _densities; set => _densities = value; }

    [Range(-1f, 1f)]
    public float isolevel;

    [Range(1, 64)]
    public int chunkSize;

    private void Start()
    {
        _densities = new NativeArray<float>((chunkSize + 1) * (chunkSize + 1), Allocator.Temp);
        for (int x = 0; x < chunkSize + 1; x++)
        {
            for (int y = 0; y < chunkSize + 1; y++)
            {
                int index = x * (chunkSize + 1) + y;
                float density = noise.snoise(new float2(x, y) * 0.1f);
                _densities[index] = density;
            }
        }

        GetComponent<MeshFilter>().mesh = MarchingSquares.CreateMesh(_densities, chunkSize, isolevel);
        _densities.Dispose();
    }
}
