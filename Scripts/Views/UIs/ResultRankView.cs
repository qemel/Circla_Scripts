using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class ResultRankView : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void SetRank(Sprite sprite)
        {
            image.sprite = sprite;

            var rectTransform = image.GetComponent<RectTransform>();
            var sizeDelta = rectTransform.sizeDelta;
            sizeDelta.y = 512;
            sizeDelta.x = sprite.rect.width * 512 / sprite.rect.height;
            rectTransform.sizeDelta = sizeDelta;
        }
    }
}