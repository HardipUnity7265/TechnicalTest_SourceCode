using System;
using UnityEngine;

namespace TechnicalTest
{
    [Serializable]
    public class Product
    {
        public string name;
        public GameObject prefab;
        public Sprite sprite;
    }

    public class UIManager : MonoBehaviour
    {
        public static UIManager Manager;

        [Header("Ground")]
        public Transform ground;

        [Header("Panels")]
        public Product_Panel product_Panel;

        [Header("Products")]
        public Product[] products;

        private void Awake()
        {
            if (Manager == null)
                Manager = this;
            else
                Destroy(gameObject);
        }
    }
}