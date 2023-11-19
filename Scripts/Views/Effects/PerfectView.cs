using AnnulusGames.LucidTools.Audio;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views.Effects
{
    public class PerfectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI perfectText;
        [SerializeField] private float duration;
        [SerializeField] private float letterSpacing;
        public void Animate()
        {
            gameObject.SetActive(true);
            perfectText.text = "PERFECT";
            DOTween.To(() => perfectText.characterSpacing, x => perfectText.characterSpacing = x, letterSpacing,
                    duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    // 徐々にフェードアウト
                    DOTween.ToAlpha(() => perfectText.color, x => perfectText.color = x, 0, duration)
                        .SetEase(Ease.OutQuad)
                        .OnComplete(() => gameObject.SetActive(false));
                });
        }
    }
}