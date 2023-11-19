using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs.Settings
{
    [RequireComponent(typeof(Button))]
    public class MetronomeButton : MonoBehaviour
    {
        private Button _button;
        private static bool _isMetronomeActive;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() =>
            {
                _isMetronomeActive = !_isMetronomeActive;
                PlayerPrefs.SetInt("Metronome", _isMetronomeActive ? 1 : 0);
            });
        }
    }
}