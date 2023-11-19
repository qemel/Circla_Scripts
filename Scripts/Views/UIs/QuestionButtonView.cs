using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class QuestionButtonView : MonoBehaviour
    {
        [SerializeField] private PopUpSimpleUIWithCloseButtonView popUp;

        private PopUpSimpleUIWithCloseButtonView _popUpSimpleUiWithCloseButtonView;
        private Button _button;

        private bool IsPopUpActive => _popUpSimpleUiWithCloseButtonView != null;

        private void Awake()
        {
            TryGetComponent(out _button);
        }

        private void Start()
        {
            _button.onClick.AddListener(() =>
            {
                if (IsPopUpActive) Destroy(_popUpSimpleUiWithCloseButtonView.gameObject);
                else _popUpSimpleUiWithCloseButtonView = Instantiate(popUp, transform.parent);
            });
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}