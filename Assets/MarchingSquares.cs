using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public static class MarchingSquares
{
    public static Mesh CreateMesh(NativeArray<float> densities, int chunkSize, float isolevel)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                VoxelCorners<float> voxelDensities = GetVoxelDensities(position, densities, chunkSize);
                int squareIndex = CalculateSquareIndex(voxelDensities, isolevel);
                if (squareIndex == 0)
                {
                    continue; // This voxel is completely outside the surface
                }

                Vector2 bottomLeft = position + Vector2.zero;
                Vector2 bottomRight = position + Vector2.right;
                Vector2 topRight = position + Vector2.up + Vector2.right;
                Vector2 topLeft = position + Vector2.up;

                Vector2 leftEdge = position + Vector2.up * 0.5f;
                Vector2 topEdge = position + Vector2.up + Vector2.right * 0.5f;
                Vector2 rightEdge = position + Vector2.right + Vector2.up * 0.5f;
                Vector2 bottomEdge = position + Vector2.right * 0.5f;

                if (squareIndex == 1)
                {
                    vertices.Add(bottomLeft);
                    vertices.Add(leftEdge);
                    vertices.Add(bottomEdge);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if (squareIndex == 2)
                {
                    vertices.Add(bottomEdge);
                    vertices.Add(rightEdge);
                    vertices.Add(bottomRight);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if (squareIndex == 3)
                {
                    vertices.Add(leftEdge);
                    vertices.Add(bottomRight);
                    vertices.Add(bottomLeft);
                    vertices.Add(leftEdge);
                    vertices.Add(rightEdge);
                    vertices.Add(bottomRight);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if (squareIndex == 4)
                {
                    vertices.Add(topEdge);
                    vertices.Add(topRight);
                    vertices.Add(rightEdge);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if (squareIndex == 5)
                {
                    float average = (voxelDensities.Corner1 + voxelDensities.Corner2 + voxelDensities.Corner3 + voxelDensities.Corner4) / 4f;
                    if (average < isolevel)
                    {
                        vertices.Add(bottomLeft);
                        vertices.Add(leftEdge);
                        vertices.Add(bottomEdge);
                        vertices.Add(leftEdge);
                        vertices.Add(rightEdge);
                        vertices.Add(bottomEdge);
                        vertices.Add(leftEdge);
                        vertices.Add(topEdge);
                        vertices.Add(rightEdge);
                        vertices.Add(topEdge);
                        vertices.Add(topRight);
                        vertices.Add(rightEdge);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                    }
                    else
                    {
                        vertices.Add(bottomLeft);
                        vertices.Add(leftEdge);
                        vertices.Add(bottomEdge);
                        vertices.Add(topEdge);
                        vertices.Add(topRight);
                        vertices.Add(rightEdge);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                    }
                }
                else if (squareIndex == 6)
                {
                    vertices.Add(topEdge);
                    vertices.Add(topRight);
                    vertices.Add(bottomRight);
                    vertices.Add(topEdge);
                    vertices.Add(bottomRight);
                    vertices.Add(bottomEdge);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if (squareIndex == 7)
                {
                    vertices.Add(leftEdge);
                    vertices.Add(bottomRight);
                    vertices.Add(bottomLeft);
                    vertices.Add(leftEdge);
                    vertices.Add(topEdge);
                    vertices.Add(bottomRight);
                    vertices.Add(topEdge);
                    vertices.Add(topRight);
                    vertices.Add(bottomRight);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if (squareIndex == 8)
                {
                    vertices.Add(topLeft);
                    vertices.Add(topEdge);
                    vertices.Add(leftEdge);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if (squareIndex == 9)
                {
                    vertices.Add(topLeft);
                    vertices.Add(topEdge);
                    vertices.Add(bottomEdge);
                    vertices.Add(topLeft);
                    vertices.Add(bottomEdge);
                    vertices.Add(bottomLeft);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if(squareIndex == 10)
                {
                    float average = (voxelDensities.Corner1 + voxelDensities.Corner2 + voxelDensities.Corner3 + voxelDensities.Corner4) / 4f;
                    if (average < isolevel)
                    {
                        vertices.Add(topLeft);
                        vertices.Add(topEdge);
                        vertices.Add(leftEdge);
                        vertices.Add(leftEdge);
                        vertices.Add(topEdge);
                        vertices.Add(rightEdge);
                        vertices.Add(leftEdge);
                        vertices.Add(rightEdge);
                        vertices.Add(bottomEdge);
                        vertices.Add(bottomEdge);
                        vertices.Add(rightEdge);
                        vertices.Add(bottomRight);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                    }
                    else
                    {
                        vertices.Add(bottomEdge);
                        vertices.Add(rightEdge);
                        vertices.Add(bottomRight);
                        vertices.Add(topLeft);
                        vertices.Add(topEdge);
                        vertices.Add(leftEdge);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                        triangles.Add(triangles.Count);
                    }
                }
                else if(squareIndex == 11)
                {
                    vertices.Add(topLeft);
                    vertices.Add(topEdge);
                    vertices.Add(bottomLeft);
                    vertices.Add(bottomLeft);
                    vertices.Add(topEdge);
                    vertices.Add(rightEdge);
                    vertices.Add(bottomLeft);
                    vertices.Add(rightEdge);
                    vertices.Add(bottomRight);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if(squareIndex == 12)
                {
                    vertices.Add(topLeft);
                    vertices.Add(topRight);
                    vertices.Add(leftEdge);
                    vertices.Add(leftEdge);
                    vertices.Add(topRight);
                    vertices.Add(rightEdge);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if(squareIndex == 13)
                {
                    vertices.Add(bottomLeft);
                    vertices.Add(topLeft);
                    vertices.Add(bottomEdge);
                    vertices.Add(topLeft);
                    vertices.Add(rightEdge);
                    vertices.Add(bottomEdge);
                    vertices.Add(topLeft);
                    vertices.Add(topRight);
                    vertices.Add(rightEdge);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if(squareIndex == 14)
                {
                    vertices.Add(topLeft);
                    vertices.Add(topRight);
                    vertices.Add(leftEdge);
                    vertices.Add(leftEdge);
                    vertices.Add(topRight);
                    vertices.Add(bottomEdge);
                    vertices.Add(bottomEdge);
                    vertices.Add(topRight);
                    vertices.Add(bottomRight);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
                else if(squareIndex == 15)
                {
                    vertices.Add(bottomLeft);
                    vertices.Add(topLeft);
                    vertices.Add(bottomRight);
                    vertices.Add(topLeft);
                    vertices.Add(topRight);
                    vertices.Add(bottomRight);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                    triangles.Add(triangles.Count);
                }
            }
        }

        Mesh mesh = new Mesh();

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }

    private static VoxelCorners<float> GetVoxelDensities(Vector2Int position, NativeArray<float> densities, int chunkSize)
    {
        VoxelCorners<float> voxelDensities = new VoxelCorners<float>();

        for (int i = 0; i < 4; i++)
        {
            Vector2Int cornerPosition = position + SquareCorners[i];
            int index = cornerPosition.x * chunkSize + cornerPosition.y;
            voxelDensities[i] = densities[index];
        }

        return voxelDensities;
    }

    public static int CalculateSquareIndex(VoxelCorners<float> densities, float isolevel)
    {
        int cubeIndex = 0;

        if (densities.Corner1 < isolevel) { cubeIndex |= 1; }
        if (densities.Corner2 < isolevel) { cubeIndex |= 2; }
        if (densities.Corner3 < isolevel) { cubeIndex |= 4; }
        if (densities.Corner4 < isolevel) { cubeIndex |= 8; }

        return cubeIndex;
    }

    public static Vector2Int[] SquareCorners =
    {
        new Vector2Int(0, 0),
        new Vector2Int(1, 0),
        new Vector2Int(1, 1),
        new Vector2Int(0, 1)
    };
}