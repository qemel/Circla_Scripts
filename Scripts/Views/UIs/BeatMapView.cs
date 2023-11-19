using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Songs.Notes;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class BeatMapView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI artist;
        [SerializeField] private Image songFrameImage;
        

        [Range(1, 2)] [SerializeField] private float scaleCenter;


        private AudioPlayer _player;
        
        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void MoveAnimation(Vector3 position)
        {
            RectTransform
                .DOLocalMove(position, 0.5f)
                .SetEase(Ease.OutCubic);
        }

        public void ScaleCenterAnimation()
        {
            transform
                .DOScale(scaleCenter, 0.5f)
                .SetEase(Ease.OutCubic);
        }

        public void ScaleOutAnimation()
        {
            transform
                .DOScale(1f, 0.5f)
                .SetEase(Ease.OutCubic);
        }

        /// <summary>
        /// songの情報の見た目を変更する
        /// </summary>
        public void SetCurrentSong(BeatMapScriptableObject beatMapScriptableObject)
        {
            _player?.Stop(0.5f);
            image.sprite = beatMapScriptableObject.Sprite;
            title.text = beatMapScriptableObject.Title;
            artist.text = beatMapScriptableObject.Artist;
            SetFrameColor(beatMapScriptableObject.FrameColor);
        }

        private void SetFrameColor(Color color)
        {
            songFrameImage.color = color;
        }
        
        private void OnDestroy()
        {
            _player?.Stop();
        }
    }
}