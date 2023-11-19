using App.Scripts.Models.Songs.Notes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class BeatMapInResultView : MonoBehaviour
    {
        [SerializeField] private Image songImage;
        [SerializeField] private Image songBGImage;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI difficultyText;

        public void SetCurrentSong(Sprite sprite, string title, DifficultyType diff)
        {
            songImage.sprite = sprite;
            songBGImage.sprite = sprite;
            titleText.text = title;
            difficultyText.text = diff.ToString();
        }
    }
}