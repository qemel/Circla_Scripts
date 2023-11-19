using System;
using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Presenters.Inputs;
using App.Scripts.Utils;
using App.Scripts.Views.UIs;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Presenters
{
    public class MenuInputService : MonoBehaviour
    {
        [SerializeField] private MenuInputProvider input;
        [SerializeField] private PopUpSimpleUIView popUpView;
        [SerializeField] private Transform popUpParent;


        [Range(0, 1)] [SerializeField] private float timeSec;
        [SerializeField] private AudioClip playSE;
        [SerializeField] private AudioClip errSE;
        [SerializeField] private AudioClip settingSE;
        [SerializeField] private AudioClip changeDifficultySE;

        private int TimeMs => (int)(timeSec * 1000);

        private BeatMapRepository _beatMapRepository;

        private static bool IsSubActive
        {
            get
            {
                var sceneCount = SceneManager.sceneCount;

                for (var i = 0; i < sceneCount; i++)
                {
                    var scene = SceneManager.GetSceneAt(i);

                    if (scene.name == SceneId.SettingSub.ToString() && scene.isLoaded) return true;
                }

                return false;
            }
        }

        private static bool _isInputEnabled;


        public void Initialize(BeatMapRepository repo)
        {
            _beatMapRepository = repo;
            Register();
            _isInputEnabled = true;
        }

        private void Register()
        {
            input.Left.Merge(input.Right)
                .ThrottleFirst(TimeSpan.FromMilliseconds(TimeMs))
                .Subscribe(x =>
                {
                    if (IsSubActive) return;
                    if (!_isInputEnabled) return;
                    switch (x)
                    {
                        case ArrowInput.Left:
                            _beatMapRepository.MoveIndexToPrevious();
                            break;
                        case ArrowInput.Right:
                            _beatMapRepository.MoveIndexToNext();
                            break;
                        case ArrowInput.None:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(x), x, null);
                    }
                }).AddTo(this);

            input.Enter.DistinctUntilChanged().Where(x => x)
                .Subscribe(_ =>
                {
                    if (IsSubActive) return;
                    if (!_isInputEnabled) return;
                    OnEnterMenu();
                }).AddTo(this);
            input.Tab.DistinctUntilChanged().Where(x => x)
                .Subscribe(_ =>
                {
                    if (IsSubActive) return;
                    if (!_isInputEnabled) return;
                    LucidAudio.PlaySE(changeDifficultySE).SetTimeSamples(4400);
                    _beatMapRepository.MoveDifficultyToNext();
                }).AddTo(this);
            input.Esc.DistinctUntilChanged().Where(x => x)
                .Subscribe(_ =>
                {
                    if (!_isInputEnabled) return;
                    LucidAudio.PlaySE(settingSE).SetTimeSamples(4400);
                    OnEscMenu();
                }).AddTo(this);
        }

        private void OnEnterMenu()
        {
            if (SelectingBeatMapSO.ChartTextAsset == null)
            {
                MyLogger.LogMenuUi("譜面が選択されていません");
                return;
            }

            if (_beatMapRepository.Current().FindInfoByType(_beatMapRepository.DiffType.Value) == null)
            {
                Instantiate(popUpView, popUpParent).Init("有効な譜面ではありません", "この難易度は実装されていません！");
                LucidAudio.PlaySE(errSE).SetTimeSamples(4400);
                return;
            }

            _isInputEnabled = false;
            LucidAudio.PlaySE(playSE).OnComplete(() =>
            {
                FadeManager.Instance.LoadScene(SceneId.Game.ToString(), 1f);
            });
        }

        private static void OnEscMenu()
        {
            if (SceneManager.GetSceneByName(SceneId.SettingSub.ToString()).isLoaded)
            {
                SceneManager.UnloadSceneAsync(SceneId.SettingSub.ToString());
                return;
            }

            SceneManager.LoadSceneAsync(SceneId.SettingSub.ToString(), LoadSceneMode.Additive);
        }
    }
}