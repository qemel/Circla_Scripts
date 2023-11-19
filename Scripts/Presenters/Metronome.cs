using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Utils;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public class Metronome
    {
        private readonly GameTimer _gameTimer;

        private readonly AudioClip _metronomeSound;

        private int _beatCount;
        private readonly float _oneBeatSec;

        private static bool IsActive
        {
            get => PlayerPrefs.GetInt("Metronome", 0) == 1;
            set => PlayerPrefs.SetInt("Metronome", value ? 1 : 0);
        }

        public Metronome(GameTimer gameTimer, Chart chart, AudioClip metronomeSound)
        {
            _gameTimer = gameTimer;
            _metronomeSound = metronomeSound;
            _oneBeatSec = MusicMath.Get1MeasureSec(chart.Bpm) / 4f;
        }

        public void Tick()
        {
            if (!IsActive) return;
            if (_gameTimer.SongTimeSec < 0f) return;
            if (_gameTimer.SongTimeSec > _oneBeatSec * _beatCount)
            {
                _beatCount++;
                LucidAudio.PlaySE(_metronomeSound);
            }
        }
    }
}