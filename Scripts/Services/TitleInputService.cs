using System.Collections;
using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Presenters.Inputs;
using App.Scripts.Views.UIs;
using App.Scripts.Views.UIs.BackGround;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static App.Scripts.Views.UIs.PopUpResult;

namespace App.Scripts.Presenters
{
    public class TitleInputService : MonoBehaviour
    {
        [SerializeField] private TitleInputPresenter input;
        [SerializeField] private PlayerInput playerInput;

        [SerializeField] private AudioClip playSE;
        [SerializeField] private AudioClip settingSE;


        [SerializeField] private LoadingUIView loadingUIView;
        [SerializeField] private BeatMapScriptableObject tutorial;

        [SerializeField] private BackgroundGenerator bgGen;


        [SerializeField] private PopUpUIView popUpPrefab;
        [SerializeField] private Transform popUpParent;


        private PopUpUIView _popUpView;

        private bool IsPopUpOpening => _popUpView != null;

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

        private void Start()
        {
            bgGen.ReGenerate();

            input.Enter.DistinctUntilChanged().Where(x => x)
                .Subscribe(_ =>
                {
                    if (IsSubActive) return;
                    if (IsPopUpOpening) return;
                    LucidAudio.PlaySE(playSE).SetTimeSamples(4400);
                    _popUpView = Instantiate(popUpPrefab, popUpParent);
                    _popUpView.Init(playerInput, "チュートリアル", "チュートリアルを始めますか？（初めての方はプレイ推奨）", true).Forget();
                    _popUpView.OnSelected.Subscribe(x =>
                    {
                        LucidAudio.PlaySE(playSE).SetTimeSamples(4400);
                        StartCoroutine(LoadScene(x is Yes));
                    }).AddTo(this);
                }).AddTo(this);


            input.Esc.DistinctUntilChanged().Where(x => x)
                .Subscribe(_ =>
                {
                    LucidAudio.PlaySE(settingSE).SetTimeSamples(4400);
                    if (SceneManager.GetSceneByName(SceneId.SettingSub.ToString()).isLoaded)
                    {
                        SceneManager.UnloadSceneAsync(SceneId.SettingSub.ToString());
                        return;
                    }

                    SceneManager.LoadSceneAsync(SceneId.SettingSub.ToString(), LoadSceneMode.Additive);
                }).AddTo(this);

            input.Reset.DistinctUntilChanged().Where(x => x)
                .Subscribe(_ =>
                {
                    LucidAudio.PlaySE(playSE).SetTimeSamples(4400);
                    bgGen.ReGenerate();
                }).AddTo(this);
        }

        private IEnumerator LoadScene(bool isTutorial = false)
        {
            loadingUIView.Show();

            if (isTutorial)
            {
                SelectingBeatMapSO.SetCurrent(tutorial, DifficultyType.Easy);
                SceneManager.LoadSceneAsync(SceneId.Game.ToString());
            }
            else
            {
                var handle = SceneManager.LoadSceneAsync(SceneId.Menu.ToString());

                while (!handle.isDone)
                {
                    loadingUIView.SetProgress(handle.progress);
                    yield return null;
                }
            }
        }
    }
}