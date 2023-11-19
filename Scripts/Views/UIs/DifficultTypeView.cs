using App.Scripts.Models.Songs.Notes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class DifficultTypeView : MonoBehaviour
    {
        [FormerlySerializedAs("image")] [SerializeField]
        private Image mainFrame;

        [SerializeField] private Image subFrame;


        [SerializeField] private TextMeshProUGUI diff;
        [SerializeField] private Sprite easyImage;
        [SerializeField] private Sprite hardImage;
        [SerializeField] private Sprite expertImage;

        [SerializeField] private Sprite easySubImage;
        [SerializeField] private Sprite hardSubImage;
        [SerializeField] private Sprite expertSubImage;


        public void SetDifficultType(DifficultyType difficultType)
        {
            diff.text = difficultType.ToString();
            mainFrame.sprite = difficultType switch
            {
                DifficultyType.Easy => easyImage,
                DifficultyType.Hard => hardImage,
                DifficultyType.Expert => expertImage,
                _ => mainFrame.sprite
            };
            
            subFrame.sprite = difficultType switch
            {
                DifficultyType.Easy => easySubImage,
                DifficultyType.Hard => hardSubImage,
                DifficultyType.Expert => expertSubImage,
                _ => subFrame.sprite
            };
        }
    }
}