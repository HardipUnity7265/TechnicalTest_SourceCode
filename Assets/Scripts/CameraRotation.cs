using UnityEngine;

namespace TechnicalTest
{
    public class CameraRotation : MonoBehaviour
    {
        //	Smoothed Orbit X and Y inputs if you wanna use ''Smooth Orbit''.
        public float smoothedOrbitX;
        public float smoothedOrbitY;

        //	Orbit X and Y speeds.
        public float orbitXSpeed = 10f;
        public float orbitYSpeed = 5f;

        // Minimum and maximum Orbit X, Y degrees.
        public float minOrbitY = 20f;
        public float maxOrbitY = 80f;

        //	Orbit transform.
        private Quaternion orbitRotation;

        // Orbit X and Y inputs.
        public float orbitX = 0f;
        public float orbitY = 0f;

        public float lerpSpeed = 2;

        private void OnEnable()
        {
            InputManager.Origin += InputManager_Origin;
        }

        private void InputManager_Origin()
        {
            if (CameraController.controller.isRotate)
                FPS_1();
        }

        private void OnDisable()
        {
            InputManager.Origin -= InputManager_Origin;
        }

        void FPS_1()
        {
            if (Input.GetMouseButton(1))
            {
                smoothedOrbitX += Input.GetAxis("Mouse X") * (orbitXSpeed * 10f) * Time.deltaTime;
                smoothedOrbitY -= Input.GetAxis("Mouse Y") * (orbitYSpeed * 10f) * Time.deltaTime;
            }

            smoothedOrbitY = Mathf.Clamp(smoothedOrbitY, minOrbitY, maxOrbitY);

            orbitX = Mathf.Lerp(orbitX, smoothedOrbitX, Time.deltaTime * 3f);
            orbitY = Mathf.Lerp(orbitY, smoothedOrbitY, Time.deltaTime * 3f);
            orbitY = Mathf.Clamp(orbitY, minOrbitY, maxOrbitY);
            orbitRotation = Quaternion.Euler(orbitY, -orbitX, 0);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, orbitRotation, Time.deltaTime * lerpSpeed);
        }
    }
}