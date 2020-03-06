using Eldemarkki.MarchingSquares.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Eldemarkki.MarchingSquares
{
    public class World : MonoBehaviour
    {
        [Header("World settings")]
        [SerializeField] private int worldWidth = 2;
        [SerializeField] private int worldHeight = 2;

        [Header("Chunk settings")]
        [Range(1, 64), SerializeField] private int chunkSize = 16;
        [SerializeField] private Chunk chunkPrefab = null;

        [Header("Marching Cubes settings")]
        [Range(-1f, 1f), SerializeField] private float isolevel = 0;

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
                    chunk.Initialize(chunkCoordinate, chunkSize, isolevel, chunkCoordinate.ToFloat2() * chunkSize);
                    chunks.Add(chunkCoordinate, chunk);
                }
            }
        }

        public bool TryGetChunkByWorldPosition(Vector2Int chunkPosition, out Chunk chunk)
        {
            Vector2Int chunkCoordinate = PositionToChunkCoordinate(chunkPosition);
            return chunks.TryGetValue(chunkCoordinate, out chunk);
        }

        public Vector2Int PositionToChunkCoordinate(Vector2Int chunkPosition)
        {
            return VectorUtilities.FloorToNearestX(chunkPosition, chunkSize) / chunkSize;
        }

        public void SetDensity(Vector2Int densityWorldPosition, float newDensity)
        {
            List<Vector2Int> modifiedChunkPositions = new List<Vector2Int>();
            for (int i = 0; i < 4; i++)
            {
                Vector2Int chunkPos = chunkSize * PositionToChunkCoordinate(densityWorldPosition - MarchingSquares.SquareCorners[i].ToVector2Int());
                if (modifiedChunkPositions.Contains(chunkPos)) { continue; }

                if (TryGetChunkByWorldPosition(chunkPos, out Chunk chunk))
                {
                    Vector2Int localPos = VectorUtilities.Mod(densityWorldPosition - chunkPos, chunkSize + 1);
                    chunk.SetDensity(localPos, newDensity);
                    modifiedChunkPositions.Add(chunkPos);
                }
            }
        }

        public float GetDensity(Vector2Int densityWorldPosition)
        {
            if (TryGetChunkByWorldPosition(densityWorldPosition, out Chunk chunk))
            {
                return chunk.GetDensity(VectorUtilities.Mod(densityWorldPosition, chunkSize));
            }

            return 0;
        }
    }
}