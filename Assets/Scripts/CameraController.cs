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
            Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            // Zoom
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                float zoomAmount = Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
                transform.position += (Vector3)(mouseWorldPosition - (Vector2)transform.position) * (zoomAmount / cam.orthographicSize);
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - zoomAmount, minCameraSize, maxCameraSize);
            }

            // Dragging movement
            if (Input.GetMouseButtonDown(2))
            {
                startDragMousePosition = mouseWorldPosition;
            }

            if (Input.GetMouseButton(2))
            {
                transform.position -= (Vector3)(mouseWorldPosition - startDragMousePosition);
            }
        }
    }
}