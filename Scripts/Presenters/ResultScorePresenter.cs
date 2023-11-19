using System.Collections.Generic;
using System.Linq;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Models.Songs.Notes.Plays;
using App.Scripts.Views.UIs;

namespace App.Scripts.Presenters
{
    public class ResultScorePresenter
    {
        private readonly ResultScoreView _resultScoreView;
        private readonly ResultRankView _resultRankView;
        private readonly BeatMapInResultView _beatMapView;

        private readonly RankScriptableObject[] _ranks;

        public ResultScorePresenter(ResultScoreView resultScoreView, ResultRankView resultRankView,
            BeatMapInResultView beatMapView,
            IEnumerable<RankScriptableObject> ranks)
        {
            _resultScoreView = resultScoreView;
            _resultRankView = resultRankView;
            _beatMapView = beatMapView;
            _ranks = ranks.OrderByDescending(x => x.LowerBound).ToArray();
        }

        public void Initialize()
        {
            SaveHighScore();
            var acc = PlayRecordCandidate.Value.Accuracy;
            acc = (int)(acc * 100) / 100f;
            _resultScoreView.SetResult(PlayRecordCandidate.Value);
            _beatMapView.SetCurrentSong(SelectingBeatMapSO.Sprite, SelectingBeatMapSO.Title,
                SelectingBeatMapSO.DifficultyType);
            foreach (var so in _ranks)
            {
                if (acc * 100 < so.LowerBound) continue;
                _resultRankView.SetRank(so.Sprite);
                return;
            }
        }

        private void SaveHighScore()
        {
            var isHighScore = RecordRepository.SaveHighScore(SelectingBeatMapSO.Title,
                SelectingBeatMapSO.DifficultyType,
                PlayRecordCandidate.Value);

            if (isHighScore) _resultScoreView.ActivateRecordImage();
        }
    }
}