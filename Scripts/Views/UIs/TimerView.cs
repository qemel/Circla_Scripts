using App.Scripts.Presenters;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private GameTimer _timer;

        public void Initialize(GameTimer timer)
        {
            _timer = timer;
        }

        private void Update()
        {
            if (_timer.SongTimeSec >= 0)
                SetTimerText(_timer.SongTimeSec);
        }

        private void SetTimerText(float time)
        {
            timerText.text = time.ToString("F2");
        }
    }
}