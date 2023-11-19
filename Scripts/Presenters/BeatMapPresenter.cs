using System.Linq;
using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Models.Songs.Notes.Plays;
using App.Scripts.Views.UIs;
using UniRx;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public class BeatMapPresenter : MonoBehaviour
    {
        private BeatMapRepository _beatMapRepository;
        private RecordView _recordView;
        private StarParent _starParent;
        private CenterSongView _centerSongView;
        private DifficultTypeView _difficultTypeView;
        [SerializeField] private BeatMapView viewPrefab;
        [SerializeField] private SongTitleFrameView songTitleFrameView;

        [SerializeField] private Transform beatMapParent;


        [SerializeField] private AudioClip changeSongSE;

        [SerializeField] private int margin;


        private BeatMapView[] _views;
        private Vector3[] _positions;

        private AudioPlayer _player;

        private static int CenterIndex => 3;

        public void Initialize(BeatMapRepository beatMapRepository, RecordView view, StarParent starParent,
            DifficultTypeView difficultTypeView, CenterSongView centerSongView)
        {
            _positions = Enumerable
                .Range(0, 7)
                .Select(i => new Vector3((i - CenterIndex) * margin, 0))
                .ToArray();
            
            // position[2]とposition[4]はもう少し外側にする
            _positions[2] = new Vector3(_positions[2].x - 50, 0);
            _positions[4] = new Vector3(_positions[4].x + 50, 0);
            
            _views = Enumerable
                .Range(0, 7)
                .Select(i => Instantiate(viewPrefab, _positions[i], Quaternion.identity, beatMapParent))
                .Select((v, i) =>
                {
                    v.RectTransform.anchoredPosition = _positions[i];
                    return v;
                })
                .ToArray();

            _beatMapRepository = beatMapRepository;
            _recordView = view;
            _starParent = starParent;
            _difficultTypeView = difficultTypeView;
            _centerSongView = centerSongView;

            PostInitialize();
        }

        private void PostInitialize()
        {
            PlayCurrentDemo();
            RenewCurrentTitleUI();

            _views[CenterIndex].ScaleCenterAnimation();

            for (var i = 0; i < _views.Length; i++)
            {
                var so = _beatMapRepository.GetByRelativeIndex(i - CenterIndex);
                _views[i].SetCurrentSong(so);
            }

            _beatMapRepository.IsIndexMoveToRight.Subscribe(OnMoveIndex).AddTo(this);

            _beatMapRepository.DiffType.DistinctUntilChanged().Subscribe(type =>
            {
                var songSo = _beatMapRepository.Current();
                var songDiffInfo = songSo.FindInfoByType(type);
                if (songDiffInfo != null)
                {
                    _starParent.SetStar(songDiffInfo.Level);
                    SelectingBeatMapSO.SetCurrent(songSo, type);
                }
                else
                {
                    _starParent.SetStar(0);
                }

                _difficultTypeView.SetDifficultType(type);

                RenewCurrentRecordUI();
            }).AddTo(this);
        }

        private void OnMoveIndex(bool isRightMove)
        {
            LucidAudio.PlaySE(changeSongSE);
            PlayCurrentDemo();
            RenewCurrentSongUIs(isRightMove);
            RenewCurrentRecordUI();
            RenewCurrentTitleUI();
            _starParent.SetStar(
                _beatMapRepository.Current().FindInfoByType(_beatMapRepository.DiffType.Value) != null
                    ? _beatMapRepository.Current().FindInfoByType(_beatMapRepository.DiffType.Value).Level
                    : 0);
            songTitleFrameView.OnChangeSong();
        }

        private void RenewCurrentTitleUI()
        {
            _centerSongView.SetCurrentSong(_beatMapRepository.Current());
        }

        private void RenewCurrentRecordUI()
        {
            var songSo = _beatMapRepository.Current();
            var diffType = _beatMapRepository.DiffType.Value;
            var record = RecordRepository.GetRecord((songSo.Title, diffType));
            _recordView.Set(record);
        }

        private void RenewCurrentSongUIs(bool isRightMove)
        {
            if (isRightMove)
            {
                for (var i = _views.Length - 1; i > 0; i--)
                {
                    _views[i].MoveAnimation(_positions[i - 1]);
                    if (i - 1 == CenterIndex)
                        _views[i].ScaleCenterAnimation();
                    else
                        _views[i].ScaleOutAnimation();
                }

                Destroy(_views[0].gameObject);

                for (var i = 0; i < _views.Length - 1; i++)
                    _views[i] = _views[i + 1];

                _views[^1] = Instantiate(viewPrefab, _positions[^1], Quaternion.identity, beatMapParent);
                _views[^1].RectTransform.anchoredPosition = _positions[^1];
                _views[^1].transform.localScale = new Vector3(1, 1, 1);
                _views[^1].SetCurrentSong(_beatMapRepository.GetByRelativeIndex(3));
            }
            else
            {
                for (var i = 0; i < _views.Length - 1; i++)
                {
                    _views[i].MoveAnimation(_positions[i + 1]);
                    if (i + 1 == CenterIndex)
                        _views[i].ScaleCenterAnimation();
                    else
                        _views[i].ScaleOutAnimation();
                }

                Destroy(_views[^1].gameObject);

                for (var i = _views.Length - 1; i > 0; i--)
                    _views[i] = _views[i - 1];

                _views[0] = Instantiate(viewPrefab, _positions[0], Quaternion.identity, beatMapParent);
                _views[0].RectTransform.anchoredPosition = _positions[0];
                _views[0].transform.localScale = new Vector3(1, 1, 1);
                _views[0].SetCurrentSong(_beatMapRepository.GetByRelativeIndex(-3));
            }
        }

        private void PlayCurrentDemo()
        {
            _player?.Stop();
            _player = LucidAudio.PlayBGM(_beatMapRepository.Current().DemoMusic, 0.8f)
                .SetLoop().SetLink(gameObject);
        }
    }
}