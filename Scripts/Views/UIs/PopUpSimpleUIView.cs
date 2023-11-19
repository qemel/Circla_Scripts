using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class PopUpSimpleUIView : MonoBehaviour
    {
        [SerializeField] private float showDuration;
        [SerializeField] private float deSpawnAnimationDuration;


        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI contextText;
        

        private async UniTaskVoid Awake()
        {
            await UniTask.WaitForSeconds(showDuration);
            transform.DOScale(new Vector3(0, 0, 0), deSpawnAnimationDuration).SetEase(Ease.InOutSine)
                .OnComplete(() => Destroy(gameObject))
                .SetLink(gameObject);
        }

        public void Init(string title, string message)
        {
            titleText.text = title;
            contextText.text = message;
        }
    }
}