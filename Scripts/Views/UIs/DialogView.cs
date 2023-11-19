using System.Collections.Generic;
using App.Scripts.Models.Songs.Notes;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI content;
        [SerializeField] private Transform imageParent;
        [SerializeField] private KeyUIView viewPrefab;

        [SerializeField] private int margin;


        private List<KeyUIView> _initViews = new();

        public void Show(DialogContent dialogContent)
        {
            _initViews.ForEach(x => x.DoActiveAnimation(false));
            _initViews.Clear();

            content.text = dialogContent.Content;
            DoSetActive(dialogContent.IsActive);
            if (dialogContent.KeyImages == null) return;

            for (var i = 0; i < dialogContent.KeyImages.Length; i++)
            {
                var fixedI = (i + 1) - (dialogContent.KeyImages.Length + 1) / 2f;
                var image = dialogContent.KeyImages.Length > i ? dialogContent.KeyImages[i] : null;
                var key = Instantiate(viewPrefab, imageParent);
                _initViews.Add(key);
                key.SetImage(image);
                key.SetPosition(new Vector2(fixedI * (float)margin, 0f));
            }
        }

        private void DoSetActive(bool isActive)
        {
            if (isActive)
                gameObject.SetActive(true);
            transform.DOScale(isActive ? 1f : 0f, 0.1f)
                .SetEase(Ease.InOutSine)
                .SetLink(gameObject).onComplete += () => gameObject.SetActive(isActive);
        }
    }
}