using App.Scripts.Models.Songs.Notes.Plays;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    /// <summary>
    /// プレイの結果を表示する
    /// </summary>
    public class ResultScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resultScoreText;
        [SerializeField] private TextMeshProUGUI accText;
        [SerializeField] private TextMeshProUGUI comboText;


        [SerializeField] private Image recordImage;

        public void SetResult(Record record)
        {
            resultScoreText.text = record.Score.ToString("000000");
            accText.text = $"{record.Accuracy * 100:F2}%";
            comboText.text = record.MaxCombo.ToString();
        }

        public void ActivateRecordImage()
        {
            recordImage.gameObject.SetActive(true);
        }
    }
}