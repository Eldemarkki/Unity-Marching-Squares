﻿using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Eldemarkki.MarchingSquares
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Chunk : MonoBehaviour
    {
        private NativeArray<float> _densities;

        private MeshFilter meshFilter;
        private Vector2Int coordinate;
        private int chunkSize;
        private float isolevel;

        private float2 noiseOffset;

        private bool isDirty;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            _densities = new NativeArray<float>((chunkSize + 1) * (chunkSize + 1), Allocator.Persistent);
            for (int y = 0; y < chunkSize + 1; y++)
            {
                for (int x = 0; x < chunkSize + 1; x++)
                {
                    int index = y * (chunkSize + 1) + x;
                    float density = noise.snoise((new float2(x, y) + noiseOffset) * 0.1f);
                    _densities[index] = density;
                }
            }

            meshFilter.sharedMesh = MarchingSquares.CreateMesh(_densities, chunkSize, isolevel);
        }

        private void Update()
        {
            if (isDirty)
            {
                meshFilter.sharedMesh = MarchingSquares.CreateMesh(_densities, chunkSize, isolevel);
                isDirty = false;
            }
        }

        private void OnDestroy()
        {
            _densities.Dispose();
        }

        public void Initialize(Vector2Int chunkCoordinate, int chunkSize, float isolevel, float2 noiseOffset)
        {
            coordinate = chunkCoordinate;
            this.chunkSize = chunkSize;
            this.isolevel = isolevel;
            this.noiseOffset = noiseOffset;
        }

        public void SetDensity(Vector2Int densityPosition, float newDensity)
        {
            _densities[densityPosition.y * (chunkSize + 1) + densityPosition.x] = Mathf.Clamp(newDensity, -1, 1);
            isDirty = true;
        }

        public float GetDensity(Vector2Int densityPosition)
        {
            return _densities[densityPosition.y * (chunkSize + 1) + densityPosition.x];
        }
    }
}