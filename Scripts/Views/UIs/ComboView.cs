using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI comboText;

        private Sequence _sequence;

        private void Awake()
        {
            // bounce animation sequence
            _sequence = DOTween.Sequence();
            _sequence.Append(comboText.transform.DOScale(1.3f, 0.1f));
            _sequence.Append(comboText.transform.DOScale(1f, 0.1f));

            _sequence.SetAutoKill(false);
            _sequence.Pause();
        }

        public void Set(int combo)
        {
            comboText.text = combo.ToString();
            _sequence.Restart();
        }
    }
}