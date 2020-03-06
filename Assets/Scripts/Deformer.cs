using Eldemarkki.MarchingSquares.Utilities;
using UnityEngine;

namespace Eldemarkki.MarchingSquares
{
    public class Deformer : MonoBehaviour
    {
        [SerializeField] private World target = null;

        [SerializeField] private float radius = 5;

        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0)) // Left mouse button
            {
                DeformTerrain(cam.ScreenToWorldPoint(Input.mousePosition), true);
            }
            else if (Input.GetMouseButton(1)) // Right mouse button
            {
                DeformTerrain(cam.ScreenToWorldPoint(Input.mousePosition), false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                radius += Input.mouseScrollDelta.y * 0.2f;
            }
        }

        private void DeformTerrain(Vector2 worldPosition, bool addTerrain)
        {
            int radiusCeiling = Mathf.CeilToInt(radius);
            Vector2Int positionFloored = VectorUtilities.Floor(worldPosition);
            int buildModifier = addTerrain ? -1 : 1;

            for (int x = -radiusCeiling; x <= radiusCeiling; x++)
            {
                for (int y = -radiusCeiling; y <= radiusCeiling; y++)
                {
                    Vector2Int offset = new Vector2Int(x, y);
                    Vector2Int offsetPosition = positionFloored + offset;

                    float distance = Vector2.Distance(worldPosition, offsetPosition);
                    if (distance <= radius)
                    {
                        float change = MathUtilities.ClampMaximum(radius - distance, radius) / radius * buildModifier;
                        target.SetDensity(offsetPosition, target.GetDensity(offsetPosition) + change);
                    }
                }
            }
        }
    }
}