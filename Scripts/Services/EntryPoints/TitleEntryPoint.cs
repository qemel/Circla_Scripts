using AnnulusGames.LucidTools.Audio;
using UnityEngine;

namespace App.Scripts.Services.EntryPoints
{
    public class TitleEntryPoint : MonoBehaviour
    {
        [SerializeField] private AudioClip bgm;

        private void Start()
        {
            var seVolume = PlayerPrefs.GetFloat("SEVolume", 0.3f);
            var bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.3f);
            LucidAudio.BGMVolume = bgmVolume;
            PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
            LucidAudio.SEVolume = seVolume;
            PlayerPrefs.SetFloat("SEVolume", seVolume);
            LucidAudio.PlayBGM(bgm).SetLoop().SetLink(gameObject);
        }
    }
}