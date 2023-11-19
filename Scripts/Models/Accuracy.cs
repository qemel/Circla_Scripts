using System;
using App.Scripts.Models.Judgements;
using App.Scripts.Views.UIs;
using UniRx;

namespace App.Scripts.Models.Songs.Notes
{
    public class Accuracy : IDisposable
    {
        public IReadOnlyReactiveProperty<float> Current => _accuracy;
        private ReactiveProperty<float> _accuracy;
        private readonly AccuracyView _accuracyView;

        public Accuracy(AccuracyView accuracyView)
        {
            _accuracyView = accuracyView;

            _accuracy = new ReactiveProperty<float>(0);
            Register();
        }

        private void Register()
        {
            _accuracy.DistinctUntilChanged().Subscribe(_ => { _accuracyView.Set(_accuracy.Value); });
        }

        public void Renew(ReactiveDictionary<IJudgement, int> judgeDict)
        {
            var total = judgeDict[new Perfect()] +
                        judgeDict[new Great()] +
                        judgeDict[new Good()] +
                        judgeDict[new Bad()] +
                        judgeDict[new Miss()];
            var accuracy = (float)(judgeDict[new Perfect()] * new Perfect().AccuracyBase +
                                   judgeDict[new Great()] * new Great().AccuracyBase +
                                   judgeDict[new Good()] * new Good().AccuracyBase +
                                   judgeDict[new Bad()] * new Bad().AccuracyBase +
                                   judgeDict[new Miss()] * new Miss().AccuracyBase) / total;
            _accuracy.Value = accuracy;
        }

        public void Dispose()
        {
            _accuracy?.Dispose();
        }
    }
}