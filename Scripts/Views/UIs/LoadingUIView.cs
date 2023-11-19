using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class LoadingUIView : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void SetProgress(float progress)
        {
            slider.value = progress;
        }
    }
}