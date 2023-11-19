using System.Collections.Generic;
using App.Scripts.Models;
using App.Scripts.Models.Judgements;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Presenters.Inputs;
using App.Scripts.Views.Entities;
using App.Scripts.Views.UIs;
using App.Scripts.Views.UIs.Judgements;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private ComboView comboView;
        [SerializeField] private AccuracyView accView;

        [SerializeField] private TimerView timerView;
        [SerializeField] private GamePlaySongUIView gamePlaySongUIView;


        [SerializeField] private GameInputProvider gameInputProvider;
        [SerializeField] private GameInputService gameInputService;

        [SerializeField] private GameTimer timer;

        [SerializeField] private PlayerMover outerMover;
        [SerializeField] private PlayerMover innerMover;

        [SerializeField] private LanePathView outerPath;
        [SerializeField] private LanePathView innerPath;
        [SerializeField] private PlayerRotBarView rotBarView;
        


        [SerializeField] private List<JudgementViewBase> judgementViewBases;

        [SerializeField] private LaneTapPresenter laneTapPreOuter;
        [SerializeField] private LaneTapPresenter laneTapPreInner;

        [SerializeField] private LaneRotPresenter laneRotPresenter;
        [SerializeField] private LaneTapPresenter laneTapPreCenter;

        [Range(-1, 1)] [SerializeField] private float playerMoveOffsetSec;

        [SerializeField] private AudioClip metroSound;

        [SerializeField] private TutorialDialogs diglogs;
        [SerializeField] private TutorialKeyImageView tutorialKeyImage;


        private LaneTimeManager _laneTimeManager;
        private Metronome _metronome;

        private CompositeDisposable _disposables = new();

        private void Awake()
        {
            var chart = new Chart(SelectingBeatMapSO.ChartTextAsset);
            if (SelectingBeatMapSO.Title == "Circla Tutorial" &&
                SelectingBeatMapSO.DifficultyType == DifficultyType.Easy)
            {
                StartTutorial();
            }

            gamePlaySongUIView.SetText($"{SelectingBeatMapSO.Title} - {SelectingBeatMapSO.DifficultyType}");

            var score = new Score(scoreView, ChoosingMod.ModPlayability.ScoreMultiplier);
            var combo = new Combo(comboView);
            var accuracy = new Accuracy(accView);
            var rotNoteSpeed = new RotNoteSpeed();
            var offset = new NoteOffset();

            var colors = new NoteColorPreference();

            var judgementRepository = new JudgementRepository(score, combo, accuracy);
            _disposables.Add(judgementRepository);
            var judgementPresenter = new JudgementPresenter(judgementRepository, judgementViewBases);
            _disposables.Add(judgementPresenter);


            _metronome = new Metronome(timer, chart, metroSound);

            gameInputProvider.Initialize();
            gameInputService.Initialize(ChoosingMod.ModPlayability);
            timer.Initialize(chart);
            timerView.Initialize(timer);

            outerMover.Initialize(timer, chart, playerMoveOffsetSec, colors.OuterColor);
            innerMover.Initialize(timer, chart, playerMoveOffsetSec, colors.InnerColor);
            rotBarView.Initialize(colors.RotColor);
            
            outerPath.Init(colors.OuterColor);
            innerPath.Init(colors.InnerColor);

            var laneList = new List<ILanePresenter>();

            laneTapPreOuter.Initialize(chart.Outer, judgementRepository, colors.OuterColor, offset);
            laneList.Add(laneTapPreOuter);
            laneTapPreInner.Initialize(chart.Inner, judgementRepository, colors.InnerColor, offset);
            laneList.Add(laneTapPreInner);
            laneRotPresenter.Initialize(chart.OuterRot, judgementRepository, colors.RotColor, rotNoteSpeed, offset, ChoosingMod.ModPlayability);
            laneList.Add(laneRotPresenter);
            laneTapPreCenter.Initialize(chart.Center, judgementRepository, Color.white, offset);
            laneList.Add(laneTapPreCenter);


            _laneTimeManager = new LaneTimeManager(laneList);
            _laneTimeManager.Initialize().Forget();
            _disposables.Add(_laneTimeManager);

            ChoosingMod.ModVisual.Reflect(laneTapPreInner, laneTapPreOuter, laneRotPresenter).Forget();
        }

        private void Update()
        {
            _metronome.Tick();
        }

        private void StartTutorial()
        {
            diglogs.gameObject.SetActive(true);
            tutorialKeyImage.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}