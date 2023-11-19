using App.Scripts.Models.Mods;
using App.Scripts.Models.Songs.Notes;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Presenters.Settings
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class AutoRotPresenter : MonoBehaviour
    {
        private Button _button;
        private Image _image;

        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private bool _isOn;

        private void Start()
        {
            _isOn = ChoosingMod.ModPlayability is AutoArts;
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _image.sprite = _isOn ? _onSprite : _offSprite;

            _button.onClick.AddListener(() =>
            {
                _isOn = !_isOn;
                if (_isOn)
                {
                    ChoosingMod.SetModPlayability(new AutoArts());
                    _image.sprite = _onSprite;
                }
                else
                {
                    ChoosingMod.SetModPlayability(new PlayablityDefault());
                    _image.sprite = _offSprite;
                }
            });
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}