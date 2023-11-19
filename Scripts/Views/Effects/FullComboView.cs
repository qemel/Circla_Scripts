using AnnulusGames.LucidTools.Audio;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views.Effects
{
    public class FullComboView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fullComboText;
        [SerializeField] private float duration;
        [SerializeField] private float letterSpacing;

        public void Animate()
        {
            gameObject.SetActive(true);
            fullComboText.text = "FULL COMBO";
            DOTween.To(() => fullComboText.characterSpacing, x => fullComboText.characterSpacing = x, letterSpacing,
                    duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    // 徐々にフェードアウト
                    DOTween.ToAlpha(() => fullComboText.color, x => fullComboText.color = x, 0, duration)
                        .SetEase(Ease.OutQuad)
                        .OnComplete(() => gameObject.SetActive(false));
                });
        }
    }
}