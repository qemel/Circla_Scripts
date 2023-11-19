using System;
using AnnulusGames.LucidTools.Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static App.Scripts.Views.UIs.PopUpResult;

namespace App.Scripts.Views.UIs
{
    public class PopUpUIView : MonoBehaviour
    {
        private PlayerInput _input;

        public IObservable<PopUpResult> OnSelected => _onSelected;
        private readonly Subject<PopUpResult> _onSelected = new();

        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        [SerializeField] private Image selectingFrameImage;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI contextText;

        [SerializeField] private Button closeButton;


        [SerializeField] private float spawnAnimationDuration;
        [SerializeField] private AudioClip uiMoveSE;


        private bool _isSelectingYes = true;
        private bool _isCloseButtonActive = false;

        public async UniTask Init(PlayerInput playerInput, string title, string message,
            bool isCloseButtonActive = false)
        {
            selectingFrameImage.enabled = false;

            _input = playerInput;
            titleText.text = title;
            contextText.text = message;
            _isCloseButtonActive = isCloseButtonActive;

            closeButton.gameObject.SetActive(_isCloseButtonActive);

            _input.enabled = true;

            // ぽよーんと登場する
            transform.DOScale(new Vector3(0, 0, 0), spawnAnimationDuration)
                .From()
                .SetEase(Ease.OutBack)
                .SetLink(gameObject);

            yesButton.onClick.AddListener(() => _onSelected.OnNext(Yes));
            noButton.onClick.AddListener(() => _onSelected.OnNext(No));
            noButton.onClick.AddListener(Close);
            closeButton.onClick.AddListener(Close);

            _onSelected.Where(x => x is No).Subscribe(_ => Close()).AddTo(this);

            await UniTask.DelayFrame(1);
            _input.actions["Left"].started += OnLeft;
            _input.actions["Right"].started += OnRight;
            _input.actions["Enter"].started += OnEnter;

            _onSelected.AddTo(this);
        }

        private void OnLeft(InputAction.CallbackContext context)
        {
            _isSelectingYes = true;
            selectingFrameImage.enabled = true;
            LucidAudio.PlaySE(uiMoveSE).SetTimeSamples(4400);
            MoveSelectingCursor(_isSelectingYes);
        }

        private void OnRight(InputAction.CallbackContext context)
        {
            _isSelectingYes = false;
            selectingFrameImage.enabled = true;
            LucidAudio.PlaySE(uiMoveSE).SetTimeSamples(4400);
            MoveSelectingCursor(_isSelectingYes);
        }

        private void OnEnter(InputAction.CallbackContext context)
        {
            _onSelected.OnNext(_isSelectingYes ? Yes : No);
        }

        private void OnDisable()
        {
            _input.actions["Left"].started -= OnLeft;
            _input.actions["Right"].started -= OnRight;
            _input.actions["Enter"].started -= OnEnter;
        }

        private void Close()
        {
            noButton.onClick.RemoveAllListeners();
            // ぽよーんと消える
            transform.DOScale(new Vector3(0, 0, 0), spawnAnimationDuration).SetEase(Ease.InBack)
                .OnComplete(() => Destroy(gameObject))
                .SetLink(gameObject);
        }

        private void MoveSelectingCursor(bool toYes)
        {
            selectingFrameImage.transform.position =
                toYes
                    ? yesButton.transform.position + Vector3.up * 75
                    : noButton.transform.position + Vector3.up * 75;
        }
    }

    public enum PopUpResult
    {
        Yes,
        No
    }
}