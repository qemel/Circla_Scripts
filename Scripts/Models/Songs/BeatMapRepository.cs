using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using App.Scripts.Utils;
using App.Scripts.Views.UIs;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace App.Scripts.Models.Songs.Notes
{
    public class BeatMapRepository
    {
        private List<BeatMapScriptableObject> BeatMapScriptableObjects { get; }
        private readonly LoadingUIView _loadingUI;
        private readonly Transform _uiParent;

        private const string BeatMapScriptableObjectPath = "SongMap";

        public BeatMapRepository(List<BeatMapScriptableObject> beatMapScriptableObjects)
        {
            BeatMapScriptableObjects = beatMapScriptableObjects;

            if (SelectingBeatMapSO.Title != null)
            {
                var previousSongName = SelectingBeatMapSO.Title;
                var previousDiffType = SelectingBeatMapSO.DifficultyType;

                var idx = BeatMapScriptableObjects.FindIndex(beatMapScriptableObject =>
                    beatMapScriptableObject.Title == previousSongName);

                if (idx == -1)
                {
                    MyLogger.LogLoad("Song not found");
                    idx = 0;
                    previousDiffType = DifficultyType.Easy;
                }

                _index = new ReactiveProperty<int>(idx);
                _diffType = new ReactiveProperty<DifficultyType>(previousDiffType);
            }
            else
            {
                _index = new ReactiveProperty<int>(0);
                _diffType = new ReactiveProperty<DifficultyType>(DifficultyType.Easy);
            }

            _onInitializedAsync.OnNext(Unit.Default);
            _onInitializedAsync.OnCompleted();
        }

        public BeatMapRepository(LoadingUIView loadingUI, Transform uiParent)
        {
            var cts = new CancellationTokenSource();
            _uiParent = uiParent;
            BeatMapScriptableObjects = new List<BeatMapScriptableObject>();
            _loadingUI = loadingUI;
            LoadBeatMapScriptableObjects(cts.Token).Forget();
        }

        private ReactiveProperty<int> _index;

        public IObservable<bool> IsIndexMoveToRight => _isIndexMoveToRight;
        private readonly Subject<bool> _isIndexMoveToRight = new();

        public IReadOnlyReactiveProperty<DifficultyType> DiffType => _diffType;
        private readonly ReactiveProperty<DifficultyType> _diffType;

        public IObservable<Unit> OnInitializedAsync => _onInitializedAsync;
        private readonly AsyncSubject<Unit> _onInitializedAsync = new();

        private async UniTaskVoid LoadBeatMapScriptableObjects(CancellationToken token = default)
        {
            var ui = Object.Instantiate(_loadingUI, _uiParent, true);
            try
            {
                var handle = Addressables.LoadAssetsAsync<BeatMapScriptableObject>(BeatMapScriptableObjectPath, null)
                    .WithCancellation(token);
                var assets = await handle;
                BeatMapScriptableObjects.AddRange(assets);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }

            _index = new ReactiveProperty<int>(0);
            _onInitializedAsync.OnNext(Unit.Default);
            _onInitializedAsync.OnCompleted();
            Object.Destroy(ui.gameObject);
            Debug.Log("end");
        }

        public BeatMapScriptableObject FindByName(string name)
        {
            return BeatMapScriptableObjects.FirstOrDefault(beatMapScriptableObject =>
                beatMapScriptableObject.name == name);
        }

        /// <summary>
        /// 相対的なインデックスを指定してBeatMapScriptableObjectを取得する
        /// </summary>
        /// <param name="relativeIndex"></param>
        /// <returns></returns>
        public BeatMapScriptableObject GetByRelativeIndex(int relativeIndex)
        {
            var index = _index.Value;
            while (relativeIndex < 0)
                relativeIndex += BeatMapScriptableObjects.Count;

            return BeatMapScriptableObjects[
                (index + relativeIndex) % BeatMapScriptableObjects.Count];
        }


        public BeatMapScriptableObject Current()
        {
            return BeatMapScriptableObjects[_index.Value];
        }

        public void MoveIndexToNext()
        {
            _index.Value = (_index.Value + 1) % BeatMapScriptableObjects.Count;
            _isIndexMoveToRight.OnNext(true);
            SelectingBeatMapSO.SetCurrent(Current(), _diffType.Value);
        }

        public void MoveIndexToPrevious()
        {
            _index.Value = (_index.Value - 1 + BeatMapScriptableObjects.Count) % BeatMapScriptableObjects.Count;
            _isIndexMoveToRight.OnNext(false);
            SelectingBeatMapSO.SetCurrent(Current(), _diffType.Value);
        }

        /// <summary>
        /// difficultyのタイプを次に変更する
        /// </summary>
        public void MoveDifficultyToNext()
        {
            _diffType.Value = DifficultyUtil.GetNextDifficultyType(_diffType.Value);
            SelectingBeatMapSO.SetCurrent(Current(), _diffType.Value);
        }
    }
}