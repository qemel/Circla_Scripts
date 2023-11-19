using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class BlinkUIView : MonoBehaviour
    {
        [SerializeField] private float scaleOut;
        [SerializeField] private float scaleIn;

        [SerializeField] private float loopDuration;

        private TextMeshProUGUI _renderer;


        private void Awake()
        {
            _renderer = GetComponent<TextMeshProUGUI>();

            transform.localScale = Vector3.one * scaleIn;

            // doblink
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(scaleOut, loopDuration));
            sequence.Append(transform.DOScale(scaleIn, loopDuration));
            sequence.SetLoops(-1).SetLink(gameObject);

            // dofade
            var sequence2 = DOTween.Sequence();
            sequence2.Append(_renderer.DOFade(1f, loopDuration));
            sequence2.Append(_renderer.DOFade(0.5f, loopDuration));
            sequence2.SetLoops(-1).SetLink(gameObject);

            // play them
            sequence.Play();
            sequence2.Play();
        }
    }
}