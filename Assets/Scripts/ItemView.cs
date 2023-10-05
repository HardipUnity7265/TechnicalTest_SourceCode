using UnityEngine;
using UnityEngine.UI;

namespace TechnicalTest
{
    public class ItemView : MonoBehaviour
    {
        public GameObject obj;
        public Image image;

        private GameObject newObject;

        public void Bind(Product product)
        {
            obj = product.prefab;
            image.sprite = product.sprite;
            transform.name = product.name;
        }

        public void Clicked()
        {
            newObject = Instantiate(obj, obj.transform.position, Quaternion.identity);

            newObject.transform.SetParent(UIManager.Manager.ground);

            newObject.SetActive(true);

            newObject.name = transform.name;
        }
    }
}