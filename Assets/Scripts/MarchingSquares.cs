using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Eldemarkki.MarchingSquares
{
    public static class MarchingSquares
    {
        public static Mesh CreateMesh(NativeArray<float> densities, int chunkSize, float isolevel)
        {
            List<Vector3> vertices = new List<Vector3>();

            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    int2 position = new int2(x, y);
                    VoxelCorners<float> voxelDensities = GetVoxelDensities(position, densities, chunkSize);
                    int squareIndex = CalculateSquareIndex(voxelDensities, isolevel);
                    if (squareIndex == 0)
                    {
                        continue; // This voxel is completely outside the surface
                    }

                    float2 bottomLeft = position + SquareCorners[0];
                    float2 bottomRight = position + SquareCorners[1];
                    float2 topRight = position + SquareCorners[2];
                    float2 topLeft = position + SquareCorners[3];

                    float2 leftEdge = VertexInterpolate(bottomLeft, topLeft, voxelDensities.Corner1, voxelDensities.Corner4, isolevel);
                    float2 topEdge = VertexInterpolate(topLeft, topRight, voxelDensities.Corner4, voxelDensities.Corner3, isolevel);
                    float2 rightEdge = VertexInterpolate(bottomRight, topRight, voxelDensities.Corner2, voxelDensities.Corner3, isolevel);
                    float2 bottomEdge = VertexInterpolate(bottomLeft, bottomRight, voxelDensities.Corner1, voxelDensities.Corner2, isolevel);

                    if (squareIndex == 1)
                    {
                        CreateTriangle(bottomLeft, leftEdge, bottomEdge, vertices);
                    }
                    else if (squareIndex == 2)
                    {
                        CreateTriangle(bottomEdge, rightEdge, bottomRight, vertices);
                    }
                    else if (squareIndex == 3)
                    {
                        CreateTriangle(leftEdge, bottomRight, bottomLeft, vertices);
                        CreateTriangle(leftEdge, rightEdge, bottomRight, vertices);
                    }
                    else if (squareIndex == 4)
                    {
                        CreateTriangle(topEdge, topRight, rightEdge, vertices);
                    }
                    else if (squareIndex == 5)
                    {
                        float average = (voxelDensities.Corner1 + voxelDensities.Corner2 + voxelDensities.Corner3 + voxelDensities.Corner4) / 4f;
                        if (average < isolevel)
                        {
                            CreateTriangle(bottomLeft, leftEdge, bottomEdge, vertices);
                            CreateTriangle(leftEdge, rightEdge, bottomEdge, vertices);
                            CreateTriangle(leftEdge, topEdge, rightEdge, vertices);
                            CreateTriangle(topEdge, topRight, rightEdge, vertices);
                        }
                        else
                        {
                            CreateTriangle(bottomLeft, leftEdge, bottomEdge, vertices);
                            CreateTriangle(topEdge, topRight, rightEdge, vertices);
                        }
                    }
                    else if (squareIndex == 6)
                    {
                        CreateTriangle(topEdge, topRight, bottomRight, vertices);
                        CreateTriangle(topEdge, bottomRight, bottomEdge, vertices);
                    }
                    else if (squareIndex == 7)
                    {
                        CreateTriangle(leftEdge, bottomRight, bottomLeft, vertices);
                        CreateTriangle(leftEdge, topEdge, bottomRight, vertices);
                        CreateTriangle(topEdge, topRight, bottomRight, vertices);
                    }
                    else if (squareIndex == 8)
                    {
                        CreateTriangle(topLeft, topEdge, leftEdge, vertices);
                    }
                    else if (squareIndex == 9)
                    {
                        CreateTriangle(topLeft, topEdge, bottomEdge, vertices);
                        CreateTriangle(topLeft, bottomEdge, bottomLeft, vertices);
                    }
                    else if (squareIndex == 10)
                    {
                        float average = (voxelDensities.Corner1 + voxelDensities.Corner2 + voxelDensities.Corner3 + voxelDensities.Corner4) / 4f;
                        if (average < isolevel)
                        {
                            CreateTriangle(topLeft, topEdge, leftEdge, vertices);
                            CreateTriangle(leftEdge, topEdge, rightEdge, vertices);
                            CreateTriangle(leftEdge, rightEdge, bottomEdge, vertices);
                            CreateTriangle(bottomEdge, rightEdge, bottomRight, vertices);
                        }
                        else
                        {
                            CreateTriangle(bottomEdge, rightEdge, bottomRight, vertices);
                            CreateTriangle(topLeft, topEdge, leftEdge, vertices);
                        }
                    }
                    else if (squareIndex == 11)
                    {
                        CreateTriangle(topLeft, topEdge, bottomLeft, vertices);
                        CreateTriangle(bottomLeft, topEdge, rightEdge, vertices);
                        CreateTriangle(bottomLeft, rightEdge, bottomRight, vertices);
                    }
                    else if (squareIndex == 12)
                    {
                        CreateTriangle(topLeft, topRight, leftEdge, vertices);
                        CreateTriangle(leftEdge, topRight, rightEdge, vertices);
                    }
                    else if (squareIndex == 13)
                    {
                        CreateTriangle(bottomLeft, topLeft, bottomEdge, vertices);
                        CreateTriangle(topLeft, rightEdge, bottomEdge, vertices);
                        CreateTriangle(topLeft, topRight, rightEdge, vertices);
                    }
                    else if (squareIndex == 14)
                    {
                        CreateTriangle(topLeft, topRight, leftEdge, vertices);
                        CreateTriangle(leftEdge, topRight, bottomEdge, vertices);
                        CreateTriangle(bottomEdge, topRight, bottomRight, vertices);
                    }
                    else if (squareIndex == 15)
                    {
                        CreateTriangle(bottomLeft, topLeft, bottomRight, vertices);
                        CreateTriangle(topLeft, topRight, bottomRight, vertices);
                    }
                }
            }

            Mesh mesh = new Mesh();

            int[] triangles = CreateTriangles(vertices.Count);

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);

            return mesh;
        }

        private static void CreateTriangle(Vector2 a, Vector2 b, Vector2 c, List<Vector3> vertices)
        {
            vertices.Add(a);
            vertices.Add(b);
            vertices.Add(c);
        }

        private static int[] CreateTriangles(int vertexCount)
        {
            int[] triangles = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                triangles[i] = i;
            }

            return triangles;
        }

        private static VoxelCorners<float> GetVoxelDensities(int2 position, NativeArray<float> densities, int chunkSize)
        {
            VoxelCorners<float> voxelDensities = new VoxelCorners<float>();

            for (int i = 0; i < 4; i++)
            {
                int2 cornerPosition = position + SquareCorners[i];
                int index = cornerPosition.y * (chunkSize + 1) + cornerPosition.x;
                voxelDensities[i] = densities[index];
            }

            return voxelDensities;
        }

        public static int CalculateSquareIndex(VoxelCorners<float> densities, float isolevel)
        {
            int cubeIndex = 0;

            if (densities.Corner1 <= isolevel) { cubeIndex |= 1; }
            if (densities.Corner2 <= isolevel) { cubeIndex |= 2; }
            if (densities.Corner3 <= isolevel) { cubeIndex |= 4; }
            if (densities.Corner4 <= isolevel) { cubeIndex |= 8; }

            return cubeIndex;
        }

        private static float2 VertexInterpolate(float2 p1, float2 p2, float v1, float v2, float isolevel)
        {
            return p1 + (isolevel - v1) * (p2 - p1) / (v2 - v1);
        }

        public static int2[] SquareCorners =
        {
            new int2(0, 0),
            new int2(1, 0),
            new int2(1, 1),
            new int2(0, 1),
        };
    }
}