using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class PopUpSimpleUIWithCloseButtonView : MonoBehaviour
    {
        [SerializeField] private float deSpawnAnimationDuration;

        [SerializeField] private Button closeButton;

        private void Awake()
        {
            transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            closeButton.onClick.AddListener(() =>
                transform.DOScale(new Vector3(0, 0, 0), deSpawnAnimationDuration).SetEase(Ease.InOutSine)
                    .OnComplete(() => Destroy(gameObject))
                    .SetLink(gameObject));
        }


        private void OnDestroy()
        {
            closeButton.onClick.RemoveAllListeners();
        }
    }
}