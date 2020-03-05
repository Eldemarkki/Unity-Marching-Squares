using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [Header("World settings")]
    [SerializeField] private int worldWidth = 2;
    [SerializeField] private int worldHeight = 2;

    [Header("Chunk settings")]
    [Range(1, 64), SerializeField] private int chunkSize = 16;
    [SerializeField] private Chunk chunkPrefab;

    [Header("Marching Cubes settings")]
    [Range(-1f, 1f), SerializeField] private float isolevel;

    private Dictionary<Vector2Int, Chunk> chunks;

    private void Awake()
    {
        chunks = new Dictionary<Vector2Int, Chunk>(worldWidth * worldHeight);
    }

    private void Start()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                Vector2Int chunkCoordinate = new Vector2Int(x, y);

                Chunk chunk = Instantiate(chunkPrefab, (Vector2)chunkCoordinate * chunkSize, Quaternion.identity, transform);
                chunk.Initialize(chunkCoordinate, chunkSize, isolevel, Vector2.zero);
                chunks.Add(chunkCoordinate, chunk);
            }
        }
    }
}
