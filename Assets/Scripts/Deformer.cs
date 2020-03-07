using Eldemarkki.MarchingSquares.Utilities;
using UnityEngine;

namespace Eldemarkki.MarchingSquares
{
    public class Deformer : MonoBehaviour
    {
        [SerializeField] private World target = null;

        [SerializeField] private float radius = 5;
        [SerializeField] private Transform radiusIndicator;

        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
            radiusIndicator.localScale = Vector3.one * radius * 2;
        }

        private void Update()
        {
            Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            radiusIndicator.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, -1);

            if (Input.GetMouseButton(0)) // Left mouse button
            {
                DeformTerrain(mouseWorldPosition, true);
            }
            else if (Input.GetMouseButton(1)) // Right mouse button
            {
                DeformTerrain(mouseWorldPosition, false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                radius += Input.mouseScrollDelta.y * 0.2f;
                radiusIndicator.localScale = Vector3.one * radius * 2;
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