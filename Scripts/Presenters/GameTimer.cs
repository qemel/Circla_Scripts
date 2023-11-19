using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Utils;
using UnityEngine;

namespace App.Scripts.Presenters
{
    /// <summary>
    /// 曲のタイマー
    /// </summary>
    public class GameTimer : MonoBehaviour
    {
        private bool _isGameStarted;
        private bool _isSongStarted;
        public float SongTimeSec { get; private set; }

        private Chart _chart;

        public void Initialize(Chart chart)
        {
            _chart = chart;
            Application.targetFrameRate = 60;
            SongTimeSec = -MusicMath.Get1MeasureSec(_chart.Bpm) * 2f;

            _isGameStarted = true;
            Time.timeScale = 1f;
        }

        private void Update()
        {
            if (SongTimeSec >= 0 && !_isSongStarted)
            {
                // 一度だけ音を流す
                LucidAudio.PlayBGM(SelectingBeatMapSO.Music).SetLink(gameObject);
                _isSongStarted = true;
            }

            if (!_isGameStarted) return;
            SongTimeSec += Time.deltaTime;
        }
    }
}