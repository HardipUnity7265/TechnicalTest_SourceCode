using UnityEngine;

namespace TechnicalTest
{
    public class WASD : MonoBehaviour
    {
        public Transform mainCamera;

        [Header("Movement")]
        [SerializeField] private float moveSpeed = 1.0f;

        [Header("Move Keys")]
        [SerializeField] private KeyCode forwardKey = KeyCode.W;
        [SerializeField] private KeyCode backKey = KeyCode.S;
        [SerializeField] private KeyCode leftKey = KeyCode.A;
        [SerializeField] private KeyCode rightKey = KeyCode.D;

        Vector3 move = Vector3.zero;

        private void OnEnable()
        {
            InputManager.WASD += InputManager_WASD;
        }

        private void InputManager_WASD()
        {
            move = Vector3.zero;

            if (Input.GetKey(forwardKey))
                move += transform.forward * moveSpeed;
            if (Input.GetKey(backKey))
                move += -transform.forward * moveSpeed;
            if (Input.GetKey(leftKey))
                move += -transform.right * moveSpeed;
            if (Input.GetKey(rightKey))
                move += transform.right * moveSpeed;

            if (CameraController.controller.isMove)
                return;

            transform.localEulerAngles = new Vector3(0, mainCamera.localEulerAngles.y, 0);

            transform.position += Time.deltaTime * move;
            mainCamera.transform.position = transform.position;
        }

        private void OnDisable()
        {
            InputManager.WASD -= InputManager_WASD;
        }
    }
}