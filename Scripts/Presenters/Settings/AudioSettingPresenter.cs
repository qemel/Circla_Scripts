using System;
using AnnulusGames.LucidTools.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace App.Scripts.Presenters.Settings
{
    public class AudioSettingPresenter : MonoBehaviour
    {
        [SerializeField] private Slider seSlider;
        [SerializeField] private Slider bgmSlider;

        private async UniTaskVoid Start()
        {
            await UniTask.DelayFrame(1);
            seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
            bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
            await UniTask.DelayFrame(1);
            seSlider.value = PlayerPrefs.GetFloat("SEVolume", 0.3f);
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.3f);
        }

        private static void OnSEVolumeChanged(float value)
        {
            if (value < 0f || value > 1f) throw new ArgumentException(nameof(value));
            PlayerPrefs.SetFloat("SEVolume", value);
            LucidAudio.SEVolume = value;
        }

        private static void OnBGMVolumeChanged(float value)
        {
            if (value < 0f || value > 1f) throw new ArgumentException(nameof(value));
            PlayerPrefs.SetFloat("BGMVolume", value);
            LucidAudio.BGMVolume = value;
        }
        
        private void OnDisable()
        {
            seSlider.onValueChanged.RemoveAllListeners();
            bgmSlider.onValueChanged.RemoveAllListeners();
        }
    }
}