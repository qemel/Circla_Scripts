using System.Linq;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Models.Songs.Notes.Plays;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class RecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI comboText;
        [SerializeField] private TextMeshProUGUI accText;
        [SerializeField] private Image rankImage;

        [SerializeField] private RankScriptableObject[] ranks;

        [SerializeField] private TextMeshProUGUI dateText;

        private void Awake()
        {
            ranks = ranks.OrderByDescending(x => x.LowerBound).ToArray();
        }

        public void Set(Record record)
        {
            rankImage.color = new Color(1, 1, 1, 1);
            var score = record.Score;
            var date = record.Date;
            var maxCombo = record.MaxCombo;
            var acc = record.Accuracy;

            foreach (var so in ranks)
            {
                if (acc * 100 < so.LowerBound) continue;
                var sprite = so.Sprite;
                rankImage.sprite = sprite;
                var rectTransform = rankImage.GetComponent<RectTransform>();
                var sizeDelta = rectTransform.sizeDelta;
                sizeDelta.y = 128;
                sizeDelta.x = sprite.rect.width * 128 / sprite.rect.height;
                rectTransform.sizeDelta = sizeDelta;
                break;
            }


            scoreText.text = score != 0 ? score.ToString() : "-";
            dateText.text = date != "" ? date : "-";
            comboText.text = maxCombo != 0 ? maxCombo.ToString() : "-";
            // 小数点以下2桁まで
            accText.text = acc != 0 ? $"{acc * 100:F2}%" : "-";

            if (record.Score == 0)
            {
                // alpha 0
                rankImage.color = new Color(1, 1, 1, 0);
            }
        }
    }
}