using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Presenters.Settings
{
    public class ColorPreferencePresenter : MonoBehaviour
    {
        [SerializeField] private ColorPreferenceScriptableObject[] colorPreferenceScriptableObjects;

        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;

        [SerializeField] private Image innerImage;
        [SerializeField] private Image outerImage;
        [SerializeField] private Image rotImage;

        [SerializeField] private AudioClip clip;


        private int _currentModIndex;
        private NoteColorPreference _noteColorPreference;

        private void Start()
        {
            _currentModIndex = ExtraSettings.ColorIndex;
            _noteColorPreference = new NoteColorPreference();
            upButton.onClick.AddListener(Up);
            downButton.onClick.AddListener(Down);
            UpdateVisual();
        }

        private void Up()
        {
            LucidAudio.PlaySE(clip).SetTimeSamples(4400);
            _currentModIndex++;
            if (_currentModIndex >= colorPreferenceScriptableObjects.Length) _currentModIndex = 0;
            ExtraSettings.ColorIndex = _currentModIndex;
            UpdateVisual();
        }

        private void Down()
        {
            LucidAudio.PlaySE(clip).SetTimeSamples(4400);
            _currentModIndex--;
            if (_currentModIndex < 0) _currentModIndex = colorPreferenceScriptableObjects.Length - 1;
            ExtraSettings.ColorIndex = _currentModIndex;
            UpdateVisual();
        }


        private void UpdateVisual()
        {
            var so = colorPreferenceScriptableObjects[_currentModIndex];
            innerImage.color = so.InnerColor;
            outerImage.color = so.OuterColor;
            rotImage.color = so.RotColor;
            _noteColorPreference.SetColor(so.InnerColor, so.OuterColor, so.RotColor);
        }

        private void OnDestroy()
        {
            upButton.onClick.RemoveAllListeners();
            downButton.onClick.RemoveAllListeners();
        }
    }
}