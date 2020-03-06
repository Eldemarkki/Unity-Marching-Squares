using Unity.Mathematics;
using UnityEngine;

namespace Eldemarkki.MarchingSquares.Utilities
{
    public static class ConversionUtilities
    {
        public static Vector2Int ToVector2Int(this int2 v)
        {
            return new Vector2Int(v.x, v.y);
        }

        public static float2 ToFloat2(this Vector2Int v)
        {
            return new float2(v.x, v.y);
        }
    }
}