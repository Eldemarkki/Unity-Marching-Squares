using UnityEngine;

namespace Eldemarkki.MarchingSquares.Utilities
{
    public static class VectorUtilities
    {
        public static Vector2Int FloorToNearestX(Vector2Int n, int x)
        {
            return new Vector2Int(MathUtilities.FloorToNearestX(n.x, x),
                                  MathUtilities.FloorToNearestX(n.y, x));
        }

        public static Vector2Int Mod(Vector2Int n, int x)
        {

            return new Vector2Int((n.x % x + x) % x,
                                  (n.y % x + x) % x);
        }

        public static Vector2Int Floor(Vector2 n)
        {
            return new Vector2Int(Mathf.FloorToInt(n.x),
                                  Mathf.FloorToInt(n.y));
        }
    }
}