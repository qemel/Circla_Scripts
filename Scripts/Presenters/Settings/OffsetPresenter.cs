using System.Globalization;
using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models;
using App.Scripts.Models.Songs.Notes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Presenters.Settings
{
    public class OffsetPresenter : MonoBehaviour
    {
        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;
        [SerializeField] private TextMeshProUGUI speedText;


        [SerializeField] private AudioClip okClip;
        [SerializeField] private AudioClip cancelClip;


        private readonly NoteOffset _offset = new();

        private void Start()
        {
            // ms の小数点以下は切り捨て
            speedText.text = _offset.GetMs().ToString("+#;-#;") + "ms";
            upButton.onClick.AddListener(() =>
            {
                var ok = _offset.AddByMs(10);
                speedText.text = _offset.GetMs().ToString("+#;-#;") + "ms";
                LucidAudio.PlaySE(ok ? okClip : cancelClip).SetTimeSamples(4400);
            });
            downButton.onClick.AddListener(() =>
            {
                var ok = _offset.AddByMs(-10);
                speedText.text = _offset.GetMs().ToString("+#;-#;") + "ms";
                LucidAudio.PlaySE(ok ? okClip : cancelClip).SetTimeSamples(4400);
            });
        }

        private void OnDestroy()
        {
            upButton.onClick.RemoveAllListeners();
            downButton.onClick.RemoveAllListeners();
        }
    }
}