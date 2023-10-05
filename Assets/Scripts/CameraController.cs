using UnityEngine;

namespace TechnicalTest
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController controller;

        [Header("CameraController")]
        public Transform cameraController;

        [Header("CameraRotate")]
        [SerializeField] private GameObject cameraOrigin;
        public bool isRotate = false;

        [Header("MinMaxZoom")]
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;
        [SerializeField] private float zoomSpeed = 10.0f;

        [Header("WASD")]
        [SerializeField] Transform wasd;

        #region Private Variable
        private Vector3 worldPos;
        private Vector3 localPos;
        private Vector2 mouseUpPos;
        private Vector2 mouseDownPos;
        private Camera mainCamera;
        private float groundDistance;
        private float mouseScroll = 0;
        private bool isZoom;
        #endregion

        [Header("PublicVariables")]
       
        public bool isMove;

        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
            controller = this;
        }

        private void OnEnable()
        {
            InputManager.ZoomInOut += InputManager_ZoomInOut;
            InputManager.Origin += InputManager_Origin;
            InputManager.Reset += InputManager_Reset;
        }

        private void InputManager_Reset()
        {
            mainCamera.transform.SetParent(cameraController);
            isRotate = false;
            isZoom = false;
            isMove = false;
            mouseScroll = 0f;
            mouseUpPos = Vector2.zero;
            mouseDownPos = Vector2.zero;
            wasd.position = transform.position;
        }

        private void InputManager_Origin()
        {
            if (Input.GetMouseButtonDown(1))
            {
                groundDistance = Vector3.Distance(transform.position, GetTerrainPos(transform.position.x, transform.position.z));
                worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, groundDistance * 2));

                worldPos.y = Mathf.Clamp(worldPos.y, -1, 100);

                cameraOrigin.transform.position = worldPos;
                mainCamera.transform.parent = cameraOrigin.transform;
                isRotate = true;
                isMove = true;
            }
        }

        private void InputManager_ZoomInOut()
        {
            if (!isZoom)
            {
                mouseUpPos = Input.mousePosition;
                mouseDownPos = Input.mousePosition;
                isZoom = true;
                isMove = true;
                isRotate = false;
                mainCamera.transform.SetParent(cameraController);
            }

            if (isZoom)
            {
                mouseDownPos = Input.mousePosition;

                if (mouseDownPos.y - mouseUpPos.y > 2)
                    mouseScroll = 0.1f;
                else if (mouseDownPos.y - mouseUpPos.y < -2)
                    mouseScroll = -0.1f;
                else
                    mouseScroll = 0;

                mouseUpPos = Input.mousePosition;

                if (transform.position.y < maxZoom && mouseScroll < 0)
                    transform.Translate(mouseScroll * zoomSpeed * Vector3.forward);
                else if (transform.position.y > minZoom && mouseScroll > 0)
                    transform.Translate(mouseScroll * zoomSpeed * Vector3.forward);

                localPos = transform.position;
                localPos.y = Mathf.Clamp(localPos.y, minZoom, maxZoom);
                transform.position = localPos;
                isMove = true;
                return;
            }
        }

        private void OnDisable()
        {
            InputManager.ZoomInOut -= InputManager_ZoomInOut;
            InputManager.Origin -= InputManager_Origin;
            InputManager.Reset -= InputManager_Reset;
        }

        public Vector3 GetTerrainPos(float x, float y)
        {
            // Create Ray Instance
            Vector3 origin = new(x, 100, y);
            Physics.Raycast(origin, Vector3.down, out RaycastHit hit, Mathf.Infinity); // Launch The Instance
            return hit.point;
        }
    }
}