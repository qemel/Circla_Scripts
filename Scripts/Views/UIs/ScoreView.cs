using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public void SetCurrent(int score)
        {
            scoreText.text = score.ToString("000000");
        }
    }
}