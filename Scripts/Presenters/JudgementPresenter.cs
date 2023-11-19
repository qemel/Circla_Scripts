using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Models.Judgements;
using App.Scripts.Views.UIs.Judgements;
using UniRx;

namespace App.Scripts.Presenters
{
    public class JudgementPresenter : IDisposable
    {
        private readonly JudgementRepository _judgementRepository;
        private readonly Dictionary<IJudgement, JudgementViewBase> _judgementViewDictionary;

        private IDisposable _disposable;

        public JudgementPresenter(JudgementRepository judgementRepository, IEnumerable<IJudgementView> judgementView)
        {
            _judgementRepository = judgementRepository;
            _judgementViewDictionary = judgementView.ToDictionary(view => view.Type, view => view as JudgementViewBase);
            Register();
        }

        private void Register()
        {
            _disposable = _judgementRepository.JudgementsDictionaryObserveReplace
                .Subscribe(x => _judgementViewDictionary[x.Key].SetJudge(x.NewValue));
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}