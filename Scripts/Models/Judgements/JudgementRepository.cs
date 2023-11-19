using System;
using System.Linq;
using System.Threading;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Models.Songs.Notes.Plays;
using UniRx;
using UnityEngine.InputSystem;

namespace App.Scripts.Models.Judgements
{
    public class JudgementRepository : IDisposable
    {
        public IObservable<DictionaryReplaceEvent<IJudgement, int>> JudgementsDictionaryObserveReplace =>
            _judgementsDictionary.ObserveReplace();

        private ReactiveDictionary<IJudgement, int> _judgementsDictionary = new();
        private readonly Score _score;
        private readonly Combo _combo;
        private readonly Accuracy _accuracy;

        private CompositeDisposable _disposables = new();

        public JudgementRepository(Score score, Combo combo, Accuracy accuracy)
        {
            Reset();
            _judgementsDictionary.AddTo(_disposables);
            _score = score;
            _combo = combo;
            _accuracy = accuracy;
            Register();
        }

        private void Register()
        {
            MessageBroker.Default.Receive<SongEventId>().Subscribe(id =>
            {
                if (id != SongEventId.End) return;

                var type = ResultType.Normal;

                if (_judgementsDictionary[new Perfect()] == _judgementsDictionary.Values.Sum())
                {
                    type = ResultType.Perfect;
                }
                else if (_judgementsDictionary[new Miss()] == 0)
                {
                    type = ResultType.FullCombo;
                }

                PlayRecordCandidate.SetCurrent(new Record(
                        _score.Current.Value,
                        _combo.MaxCombo.Value,
                        _accuracy.Current.Value,
                        DateTime.Now),
                    type);

                MessageBroker.Default.Publish(type);
            }).AddTo(_disposables);
        }

        /// <summary>
        /// 判定リストに追加し、同時にスコアを加算する
        /// </summary>
        public void Add(IJudgement judgement)
        {
            _judgementsDictionary[judgement]++;

            if (judgement is Miss)
                _combo.Reset();
            else
                _combo.Add();

            _score.Add(judgement);

            _accuracy.Renew(_judgementsDictionary);
        }

        public int GetJudgeCount(IJudgement judgeType)
        {
            return _judgementsDictionary[judgeType];
        }

        private void Reset()
        {
            _judgementsDictionary = new ReactiveDictionary<IJudgement, int>
            {
                { new Perfect(), 0 },
                { new Great(), 0 },
                { new Good(), 0 },
                { new Bad(), 0 },
                { new Miss(), 0 }
            };
        }

        public override string ToString()
        {
            return $"Perfect: {_judgementsDictionary[new Perfect()]}\n" +
                   $"Great: {_judgementsDictionary[new Great()]}\n" +
                   $"Good: {_judgementsDictionary[new Good()]}\n" +
                   $"Bad: {_judgementsDictionary[new Bad()]}\n" +
                   $"Miss: {_judgementsDictionary[new Miss()]}\n";
        }

        public void Dispose()
        {
            _score?.Dispose();
            _combo?.Dispose();
            _accuracy?.Dispose();
            _disposables?.Dispose();
        }
    }
}