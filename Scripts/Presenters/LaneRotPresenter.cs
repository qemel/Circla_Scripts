using System;
using System.Collections.Generic;
using App.Scripts.Models;
using App.Scripts.Models.Judgements;
using App.Scripts.Models.Mods;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Utils;
using App.Scripts.Views.Entities;
using UniRx;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public class LaneRotPresenter : MonoBehaviour, ILanePresenter
    {
        private Lane Lane = null;
        private JudgementRepository _judgementRepository;
        private readonly List<RotNoteView> _rotNoteViews = new();
        private RotNoteSpeed _rotNoteSpeed;
        private Color _noteColor;

        [SerializeField] private GameTimer timer;
        [SerializeField] private RotNoteView rotNoteView;
        [SerializeField] private Transform noteParent;
        private LaneType Type => Lane.Type;

        private IRotInput _inputType = new RotInputDown();

        private float SongTimeSec => timer.SongTimeSec;

        /// <summary>
        /// 大きいほど早くSpawnする
        /// </summary>
        private float NoteSpawnOffsetSec => 1.25f * Type switch
        {
            LaneType.InnerRot => 1f,
            LaneType.OuterRot => 2f,
            _ => throw new ArgumentOutOfRangeException()
        };

        /// <summary>
        /// 大きいほど遅くDestroyする
        /// </summary>
        private const float NoteDestroyOffsetSec = 0f;

        private float _noteJudgeOffsetSec;

        private int _spawnIndex;
        private int _judgeIndex;

        public bool IsEnd => _judgeIndex >= Lane.Count;


        public void Initialize(Lane lane, JudgementRepository judgementRepository, Color noteColor,
            RotNoteSpeed speed, NoteOffset noteJudgeOffset, IModOfPlayability modOfPlayability)
        {
            Lane = lane;
            _judgementRepository = judgementRepository;
            _noteJudgeOffsetSec = noteJudgeOffset.GetSec();
            _rotNoteSpeed = speed;
            _noteColor = noteColor;
            if (modOfPlayability is AutoArts) _inputType = new RotInputAll();

            LateInitialize();
        }

        private void LateInitialize()
        {
            MessageBroker.Default.Receive<LaneType>().Subscribe(data =>
            {
                if (data != Type) return;
                MyLogger.LogInput(data.ToString());
            }).AddTo(this);
            MessageBroker.Default.Receive<IRotInput>().Subscribe(type => { _inputType = type; }).AddTo(this);

            InitNoteViews();
        }

        public void Update()
        {
            CheckSpawn();
            if (_rotNoteViews.Count > 0) CheckJudgeAndDestroy();
        }

        private void InitNoteViews()
        {
            var notes = Lane.Notes;
            foreach (var n in notes)
            {
                var nView = Instantiate(rotNoteView, new Vector2(), Quaternion.identity, noteParent);
                nView.SetDirection(n.Angle);
                nView.gameObject.SetActive(false);
                _rotNoteViews.Add(nView);
            }
        }

        public void CheckSpawn()
        {
            if (_spawnIndex >= _rotNoteViews.Count) return;
            if (SongTimeSec >= Lane.Notes[_spawnIndex].TimeSec - NoteSpawnOffsetSec + _rotNoteSpeed.GetSpeed())
            {
                var cur = Lane.Notes[_spawnIndex];
                _rotNoteViews[_spawnIndex].Initialize(cur.Angle, true, _noteColor,
                    Lane.Notes[_spawnIndex].TimeSec - SongTimeSec, noteParent);
                _spawnIndex++;
            }
        }

        private void CheckJudgeAndDestroy()
        {
            if (_judgeIndex >= _rotNoteViews.Count) return;
            if (!(SongTimeSec >= Lane.Notes[_judgeIndex].TimeSec + _noteJudgeOffsetSec + NoteDestroyOffsetSec)) return;
            var judge = GetJudge(Lane.Notes[_judgeIndex], _inputType);
            _rotNoteViews[_judgeIndex].DoDestroyAnimation(judge);
            _judgementRepository.Add(judge);
            _judgeIndex++;
        }

        private static IJudgement GetJudge(Note note, IRotInput rotInput)
        {
            if (rotInput is RotInputAll) return new Perfect();
            var angle = note.Angle;
            var minAngle = rotInput.MinAngle;
            var maxAngle = rotInput.MaxAngle;
            if (IsAngleInRange(minAngle, maxAngle, angle)) return new Perfect();
            return new Miss();
        }

        private static bool IsAngleInRange(double min, double max, double target)
        {
            // 正規化
            min = (min % 360 + 360) % 360;
            max = (max % 360 + 360) % 360;
            target = (target % 360 + 360) % 360;

            if (min <= max)
            {
                return target >= min && target <= max;
            }

            // 範囲が0度をまたいでいる場合
            return target >= min || target <= max;
        }
    }
}