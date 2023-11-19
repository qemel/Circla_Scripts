using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.Views.UIs.Settings
{
    public class SettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Tween _rotTween;

        [SerializeField] private float durationSec;


        private void Awake()
        {
            _rotTween = transform.DORotate(new Vector3(0, 0, 360), durationSec, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart).SetLink(gameObject);

            _rotTween.Pause();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _rotTween.Restart();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _rotTween.Pause();
            transform.rotation = Quaternion.identity;
        }
    }
}