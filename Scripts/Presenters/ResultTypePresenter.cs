using System;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Views.Effects;
using UniRx;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public class ResultTypePresenter : MonoBehaviour
    {
        [SerializeField] private PerfectView perfect;
        [SerializeField] private FullComboView fullCombo;

        private void Start()
        {
            MessageBroker.Default.Receive<ResultType>().Subscribe(x =>
            {
                switch (x)
                {
                    case ResultType.Normal:
                        return;
                    case ResultType.Perfect:
                        perfect.Animate();
                        break;
                    case ResultType.FullCombo:
                        fullCombo.Animate();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(x), x, null);
                }
            }).AddTo(this);
        }
    }
}