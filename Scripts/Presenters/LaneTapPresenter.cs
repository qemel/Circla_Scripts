using System;
using System.Collections.Generic;
using App.Scripts.Models;
using App.Scripts.Models.Judgements;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Utils;
using App.Scripts.Views.Entities;
using UniRx;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public class LaneTapPresenter : MonoBehaviour, ILanePresenter
    {
        private Lane Lane = null;
        private Chart _chart;
        private JudgementRepository _judgementRepository;
        private Color _noteColor;
        private readonly NoteSpawnSpeed _noteSpawnSpeed = new();

        [SerializeField] private GameTimer timer;
        [SerializeField] private TapNoteView tapNoteViewPrefab;
        [SerializeField] private Transform noteParent;

        public PlayerMover Mover => mover;
        [SerializeField] private PlayerMover mover;

        public IObservable<Unit> OnSpawnedAllNotesAsync => _onSpawnedAllNotesAsync;
        private readonly AsyncSubject<Unit> _onSpawnedAllNotesAsync = new();


        private readonly List<TapNoteView> _noteViews = new();


        private LaneType Type => Lane.Type;

        private float SongTimeSec => timer.SongTimeSec;

        /// <summary>
        /// 大きいほど早くSpawnする
        /// </summary>
        private const float NoteSpawnOffsetSec = 1.2f;

        private float _noteAllOffsetSec;

        /// <summary>
        /// destroyするまでの時間
        /// </summary>
        private float _removalTime => LaneJudge.LagLimit;

        private int _spawnIndex;
        private int _judgeIndex;

        public bool IsEnd => _judgeIndex >= Lane.Count;

        public void Initialize(Lane lane, JudgementRepository judgementRepository, Color noteColor,
            NoteOffset noteOffset)
        {
            Lane = lane;
            _judgementRepository = judgementRepository;
            _noteAllOffsetSec = noteOffset.GetSec();
            _noteColor = noteColor;

            LateInitialize();
        }

        /// <summary>
        /// コンストラクタ以外の初期化処理
        /// </summary>
        private void LateInitialize()
        {
            MessageBroker.Default.Receive<LaneType>().Subscribe(data =>
            {
                if (data != Type) return;
                MyLogger.LogInput(data.ToString());
                if (!IsEnd) CheckJudge();
            }).AddTo(this);

            InitNoteViews();
        }

        private void InitNoteViews()
        {
            var notes = Lane.Notes;
            foreach (var n in notes)
            {
                var nView = Instantiate(tapNoteViewPrefab, n.position, Quaternion.identity, noteParent);
                nView.gameObject.SetActive(false);
                _noteViews.Add(nView);
            }

            _onSpawnedAllNotesAsync.OnNext(Unit.Default);
            _onSpawnedAllNotesAsync.OnCompleted();
        }

        public void Update()
        {
            CheckSpawn();
            if (_noteViews.Count > 0) CheckDestroy();
        }


        public void CheckSpawn()
        {
            if (_spawnIndex >= _noteViews.Count) return;
            if (SongTimeSec >= Lane.Notes[_spawnIndex].TimeSec - NoteSpawnOffsetSec +
                _noteSpawnSpeed.GetSpeed())
            {
                _noteViews[_spawnIndex].Init(_noteColor, noteParent);
                _spawnIndex++;
            }
        }

        public void CheckDestroy()
        {
            if (_judgeIndex >= _noteViews.Count) return;
            if (SongTimeSec >= Lane.Notes[_judgeIndex].TimeSec + _removalTime + _noteAllOffsetSec)
            {
                _noteViews[_judgeIndex].DoDestroyAnimation(new Miss());
                _judgementRepository.Add(new Miss());
                _judgeIndex++;
            }
        }

        public void CheckJudge()
        {
            if (IsEnd) throw new InvalidOperationException("Lane is end");

            var notes = Lane.Notes;
            if (notes == null) throw new ArgumentNullException(nameof(notes));

            var judgement =
                LaneJudge.GetJudge(songTimeSec: SongTimeSec - _noteAllOffsetSec, note: notes[_judgeIndex]);
            if (judgement is null)
            {
                mover.EmitParticle();
                return;
            }

            _noteViews[_judgeIndex].DoDestroyAnimation(judgement);
            _judgementRepository.Add(judgement);
            _judgeIndex++;
        }
    }
}