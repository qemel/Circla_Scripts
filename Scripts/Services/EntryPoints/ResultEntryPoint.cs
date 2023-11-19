using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Presenters;
using App.Scripts.Presenters.Inputs;
using App.Scripts.Views.UIs;
using App.Scripts.Views.UIs.BackGround;
using UnityEngine;

namespace App.Scripts.Services.EntryPoints
{
    public class ResultEntryPoint : MonoBehaviour
    {
        [SerializeField] private MenuInputProvider inputProvider;
        [SerializeField] private ResultScoreView resultScoreView;
        [SerializeField] private ResultRankView resultRankView;
        [SerializeField] private BeatMapInResultView beatMapInResultView;
        [SerializeField] private BackgroundGenerator bgGen;


        [SerializeField] private RankScriptableObject[] ranks;


        [SerializeField] private ResultInputService resultInputService;


        [SerializeField] private AudioClip bgm;


        private void Awake()
        {
            inputProvider.Initialize();
            var resultScorePresenter =
                new ResultScorePresenter(resultScoreView, resultRankView, beatMapInResultView, ranks);
            resultScorePresenter.Initialize();
            resultInputService.Initialize();

            LucidAudio.PlayBGM(bgm).SetLoop().SetLink(gameObject);
            bgGen.ReGenerate();
        }
    }
}