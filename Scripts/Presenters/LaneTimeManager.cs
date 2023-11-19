using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

namespace App.Scripts.Presenters
{
    /// <summary>
    /// 譜面の終了判定を担当
    /// </summary>
    public class LaneTimeManager : IDisposable
    {
        private readonly List<ILanePresenter> _lanePresenters;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _token;

        public LaneTimeManager(List<ILanePresenter> lanePresenters)
        {
            _lanePresenters = lanePresenters;
            _cancellationTokenSource = new CancellationTokenSource();
            _token = _cancellationTokenSource.Token;
        }

        public async UniTask Initialize()
        {
            _token.ThrowIfCancellationRequested();
            await UniTask.WaitUntil(() => _lanePresenters.All(lane => lane.IsEnd), cancellationToken: _token);
            MessageBroker.Default.Publish(SongEventId.End);

            LucidAudio.StopAllBGM(1.5f);

            //TODO: 時間指定
            await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: _token);
            _token.ThrowIfCancellationRequested();
            FadeManager.Instance.LoadScene(SceneId.Result.ToString());
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}