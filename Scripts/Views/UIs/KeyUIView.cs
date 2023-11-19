using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class KeyUIView : MonoBehaviour
    {
        [SerializeField] private Image image;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            DoFloatingAnimation();
        }

        public void DoActiveAnimation(bool isActive)
        {
            if (isActive)
                gameObject.SetActive(true);
            transform.DOScale(isActive ? 1f : 0f, 0.1f)
                .SetEase(Ease.InOutSine)
                .SetLink(gameObject).onComplete += () => gameObject.SetActive(isActive);
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
            // 縦横比を合わせる
            var size = image.sprite.rect.size;
            var aspect = size.x / size.y;
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.y * aspect, _rectTransform.sizeDelta.y);
        }

        public void SetPosition(Vector2 pos)
        {
            _rectTransform.anchoredPosition = pos;
        }

        private void DoFloatingAnimation()
        {
            _rectTransform.DOLocalMoveY(0.3f, 1.2f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject);
        }
    }
}