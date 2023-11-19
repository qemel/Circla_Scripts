using AnnulusGames.LucidTools.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class OnClickToSound : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => LucidAudio.PlaySE(clip).SetTimeSamples(4400));
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}