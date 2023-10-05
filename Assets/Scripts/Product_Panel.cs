using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TechnicalTest
{
    public class Product_Panel : MonoBehaviour
    {
        [Header("Texts")]
        public TextMeshProUGUI objectNameTxt;
        public TextMeshProUGUI objectPositionTxt;
        public TextMeshProUGUI objectRotationTxt;
        public TextMeshProUGUI objectScaleTxt;

        [Header("ItemView")]
        public ItemView itemView;
        public ScrollRect scrollRect;
        public Transform itemParent;

        public void Start()
        {
            for(int i = 0; i < UIManager.Manager.products.Length; i++)
                Instantiate(itemView, itemParent).Bind(UIManager.Manager.products[i]);
        }

        public void ShowDescription(Transform transform)
        {
            objectNameTxt.text = transform.name;
            objectPositionTxt.text = transform.position.ToString();
            objectRotationTxt.text = transform.eulerAngles.ToString();
            objectScaleTxt.text = transform.localScale.ToString();
        }
    }
}