using UnityEngine;

namespace TechnicalTest
{
    public class InputManager : MonoBehaviour
    {
        public delegate void OriginMovement();
        public static event OriginMovement Origin;

        public delegate void PanMovement();
        public static event PanMovement Panning;

        public delegate void ZoomMovement();
        public static event ZoomMovement ZoomInOut;

        public delegate void WASDMovement();
        public static event WASDMovement WASD;

        public delegate void ResetMovement();
        public static event ResetMovement Reset;

        [Header("Move Keys")]
        [SerializeField] private KeyCode controlKey = KeyCode.LeftControl;
        [SerializeField] private KeyCode shiftKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode forwardKey = KeyCode.W;
        [SerializeField] private KeyCode backKey = KeyCode.S;
        [SerializeField] private KeyCode leftKey = KeyCode.A;
        [SerializeField] private KeyCode rightKey = KeyCode.D;

        void LateUpdate()
        {
            if (Input.GetMouseButton(1) && Input.GetKey(controlKey))
            {
                Panning();
                return;
            }

            if (Input.GetMouseButton(1) && Input.GetKey(shiftKey))
            {
                ZoomInOut();
                return;
            }

            if (Input.GetKey(forwardKey) || Input.GetKey(backKey) || Input.GetKey(leftKey) || Input.GetKey(rightKey))
            {
                WASD();
                return;
            }

            if (Input.GetMouseButton(1))
            {
                Origin();
                return;
            }

            if (Input.GetMouseButtonUp(1))
                Reset();
        }
    }
}
