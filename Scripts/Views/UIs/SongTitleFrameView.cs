using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class SongTitleFrameView : MonoBehaviour
    {
        [SerializeField] private float animateDuration;
        [SerializeField] private float moveAmount;

        private RectTransform _rectTransform;
        private float _defaultPositionY;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _defaultPositionY = GetComponent<RectTransform>().anchoredPosition.y;
        }

        public void OnChangeSong()
        {
            // 一度上に動いてから戻ってくる(recttransform)
            _rectTransform.DOAnchorPosY(_defaultPositionY + moveAmount, animateDuration).SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    _rectTransform.DOAnchorPosY(_defaultPositionY, animateDuration).SetEase(Ease.InExpo);
                }).SetLink(gameObject);
        }
    }
}