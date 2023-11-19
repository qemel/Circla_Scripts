using System;
using App.Scripts.Models.Judgements;
using App.Scripts.Views.UIs;
using UniRx;
using unityroom.Api;

namespace App.Scripts.Models.Songs.Notes
{
    public class Score : IDisposable
    {
        public IReadOnlyReactiveProperty<int> Current => _current;
        private readonly ReactiveProperty<int> _current = new();
        private readonly float _scoreMultiplier;

        private readonly ScoreView _scoreView;

        public Score(ScoreView scoreView, float scoreMultiplier = 1)
        {
            _scoreView = scoreView;
            _scoreMultiplier = scoreMultiplier;
            _current.Value = 0;

            Register();
        }
        private void Register()
        {
            _current.DistinctUntilChanged().Subscribe(x =>
            {
                var score = (float)x * _scoreMultiplier;
                _scoreView.SetCurrent((int)score);
                if (SelectingBeatMapSO.Title == "No more sadistic!" && x > 0)
                {
                    UnityroomApiClient.Instance.SendScore(1, score, ScoreboardWriteMode.HighScoreDesc);
                }
            });
        }

        public void Add(IJudgement judge)
        {
            _current.Value += judge.ScoreBase;
        }

        public void Dispose()
        {
            _current?.Dispose();
        }
    }
}