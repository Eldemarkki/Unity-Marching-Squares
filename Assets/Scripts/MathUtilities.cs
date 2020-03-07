using UnityEngine;

namespace Eldemarkki.MarchingSquares.Utilities
{
    public static class MathUtilities
    {
        public static int FloorToNearestX(int n, int x)
        {
            return Mathf.FloorToInt((float)n / x) * x;
        }

        public static float ClampMaximum(float n, float maximum)
        {
            return n > maximum ? maximum : n;
        }
    }
}