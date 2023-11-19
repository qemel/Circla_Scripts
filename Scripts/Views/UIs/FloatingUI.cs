using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class FloatingUI : MonoBehaviour
    {
        [SerializeField] private float loopDuration;
        [SerializeField] private float height;

        [SerializeField] private Ease inEase;
        [SerializeField] private Ease outEase;


        private void Awake()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalMoveY( height, loopDuration / 2).SetRelative(true).SetEase(inEase));

            sequence.SetLoops(-1, LoopType.Yoyo);
        }
    }
}