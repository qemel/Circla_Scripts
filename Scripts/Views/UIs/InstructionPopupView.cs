using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class InstructionPopupView : MonoBehaviour
    {
        private PlayerInput _input;

        public IObservable<Unit> OnSelected => _onSelected;
        private readonly ISubject<Unit> _onSelected = new Subject<Unit>();

        [SerializeField] private Button yesButton;

        [SerializeField] private Image selectingFrameImage;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI contextText;

        [SerializeField] private Image instructionImage;

        [SerializeField] private float spawnAnimationDuration;
        [SerializeField] private AudioClip uiMoveSE;


        private bool _isSelectingYes = true;

        public async UniTask Init(PlayerInput playerInput, string title, string message, Image image = null)
        {
            _input = playerInput;
            titleText.text = title;
            contextText.text = message;
            if (image != null) instructionImage.sprite = image.sprite;

            _input.enabled = true;

            // ぽよーんと登場する
            transform.DOScale(new Vector3(0, 0, 0), spawnAnimationDuration)
                .From()
                .SetEase(Ease.OutBack)
                .SetLink(gameObject);

            yesButton.onClick.AddListener(() => _onSelected.OnNext(Unit.Default));

            _onSelected.Subscribe(_ => Close()).AddTo(this);

            await UniTask.DelayFrame(1);
            _input.actions["Enter"].started += OnEnter;
        }

        private void OnEnter(InputAction.CallbackContext context)
        {
            _onSelected.OnNext(Unit.Default);
        }

        private void OnDisable()
        {
            _input.actions["Enter"].started -= OnEnter;
        }

        private void Close()
        {
            // ぽよーんと消える
            transform.DOScale(new Vector3(0, 0, 0), spawnAnimationDuration).SetEase(Ease.InBack)
                .OnComplete(() => Destroy(gameObject))
                .SetLink(gameObject);
        }
    }
}