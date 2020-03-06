using UnityEngine;

namespace Eldemarkki.MarchingSquares
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [Header("Zoom")]
        [SerializeField] private float zoomSpeed = 300;
        [SerializeField] private float minCameraSize = 5;
        [SerializeField] private float maxCameraSize = 200;

        private Vector2 startDragMousePosition;

        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            // Zoom
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + -Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime, minCameraSize, maxCameraSize);
            }

            // Dragging movement
            if (Input.GetMouseButtonDown(2))
            {
                startDragMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(2))
            {
                transform.position -= (Vector3)((Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - startDragMousePosition);
            }
        }
    }
}