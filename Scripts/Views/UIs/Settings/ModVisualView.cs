using AnnulusGames.LucidTools.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs.Settings
{
    public class ModVisualView : MonoBehaviour
    {
        [SerializeField] private Image current;

        [SerializeField] private AudioClip uiMove;

        public void SetCurrent(Sprite sprite)
        {
            LucidAudio.PlaySE(uiMove).SetTimeSamples(4400);
            current.sprite = sprite;
        }
    }
}