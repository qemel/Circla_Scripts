using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Presenters.Settings
{
    public class NoteSpawnSpeedPresenter : MonoBehaviour
    {
        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;
        [SerializeField] private TextMeshProUGUI speedText;


        [SerializeField] private AudioClip okClip;
        [SerializeField] private AudioClip cancelClip;


        private readonly NoteSpawnSpeed _noteSpawnSpeed = new();

        private void Start()
        {
            speedText.text = _noteSpawnSpeed.GetLevel().ToString();
            upButton.onClick.AddListener(() =>
            {
                var ok = _noteSpawnSpeed.AddSpeed(1);
                speedText.text = _noteSpawnSpeed.GetLevel().ToString();
                LucidAudio.PlaySE(ok ? okClip : cancelClip).SetTimeSamples(4400);
            });
            downButton.onClick.AddListener(() =>
            {
                var ok = _noteSpawnSpeed.AddSpeed(-1);
                speedText.text = _noteSpawnSpeed.GetLevel().ToString();
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