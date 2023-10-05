using UnityEngine;

namespace TechnicalTest
{
    public class Panning : MonoBehaviour
    {
        public Camera mainCamera;

        [Header("Movement")]
        [SerializeField] private float panSpeed = 5f;

        [Header("Move Keys")]
        [SerializeField] private KeyCode controlKey = KeyCode.LeftControl;

        private Vector3 lastPanPosition;

        public bool isPanFirstTime = false;

        private void OnEnable()
        {
            InputManager.Panning += InputManager_Panning;
            InputManager.Reset += InputManager_Reset;
        }

        private void InputManager_Reset()
        {
            isPanFirstTime = false;
        }

        private void OnDisable()
        {
            InputManager.Panning -= InputManager_Panning;
            InputManager.Reset -= InputManager_Reset;
        }

        private void InputManager_Panning()
        {
            if (Input.GetMouseButtonDown(1) && Input.GetKey(controlKey))
            {
                mainCamera.transform.SetParent(CameraController.controller.cameraController);
                transform.position = mainCamera.transform.position;
                transform.localEulerAngles = new Vector3(0, mainCamera.transform.localEulerAngles.y, 0);

                CameraController.controller.isRotate = false;
                CameraController.controller.isMove = true;

                isPanFirstTime = true;
            }

            //camera move around planer
            if (Input.GetMouseButton(1) && Input.GetKey(controlKey))
            {
                if(!isPanFirstTime)
                {
                    lastPanPosition = Input.mousePosition;

                    mainCamera.transform.SetParent(CameraController.controller.cameraController);
                    transform.position = mainCamera.transform.position;
                    transform.localEulerAngles = new Vector3(0, mainCamera.transform.localEulerAngles.y, 0);

                    CameraController.controller.isRotate = false;
                    CameraController.controller.isMove = true;

                    isPanFirstTime = true;
                }
               
                HandleMouse();
                return;
            }
        }

        void HandleMouse()
        {
            // On mouse down, capture it's position.
            // Otherwise, if the mouse is still down, pan the camera.
            if (Input.GetMouseButtonDown(1))
                lastPanPosition = Input.mousePosition;
            else if (Input.GetMouseButton(1))
                PanCamera(Input.mousePosition);
        }

        void PanCamera(Vector3 newPanPosition)
        {
            // Determine how much to move the camera
            Vector3 offset = mainCamera.ScreenToViewportPoint(lastPanPosition - newPanPosition);
            Vector3 move = new(offset.x * panSpeed, 0, offset.y * panSpeed);

            // Perform the movement
            transform.Translate(move, Space.Self);

            // Cache the position
            lastPanPosition = newPanPosition;

            mainCamera.transform.position = transform.position;
        }
    }
}