using UnityEngine;

namespace TechnicalTest
{
    public class DragObject : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public Material forMouseEnter;
        public Material forMouseDrag;
        public Material perMaterial;

        private Vector3 mOffset;
        private float mZCoord;
        Vector3 screenPosition;

        Plane plane = new(Vector3.down, 0);

        bool isMoveAble;

        public void OnMouseDown()
        {
            mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;

            // Store offset = gameobject world pos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        }

        private Vector3 GetMouseAsWorldPoint()
        {
            // Pixel coordinates of mouse (x,y)
            Vector3 mousePoint = Input.mousePosition;

            // z coordinate of game object on screen
            mousePoint.z = mZCoord;

            // Convert it to world points
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        public void OnMouseEnter()
        {
            meshRenderer.material = forMouseEnter;
        }

        public void OnMouseExit()
        {
            meshRenderer.material = perMaterial;
        }

        private void OnMouseDrag()
        {
            meshRenderer.material = forMouseDrag;

            screenPosition = GetMouseAsWorldPoint() + mOffset;
            screenPosition.y = 0;
            transform.position = screenPosition;

            UIManager.Manager.product_Panel.ShowDescription(transform);
        }

        private void Awake()
        {
            isMoveAble = true;
        }

        void Update()
        {
            if (!isMoveAble)
                return;

            if (Input.GetMouseButton(0))
                ButtonDown();

            if (Input.GetMouseButtonUp(0))
                ButtonUp();
        }

        public void ButtonDown()
        {
            screenPosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if (plane.Raycast(ray, out float distance))
                mOffset = ray.GetPoint(distance);
            transform.position = mOffset;
        }

        public void ButtonUp()
        {
            isMoveAble = false;
        }

        
    }
}