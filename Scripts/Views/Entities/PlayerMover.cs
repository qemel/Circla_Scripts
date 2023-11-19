using App.Scripts.Models.Songs.Notes;
using App.Scripts.Presenters;
using App.Scripts.Utils;
using UnityEngine;

namespace App.Scripts.Views.Entities
{
    public class PlayerMover : MonoBehaviour
    {
        private GameTimer _songTimeOwner;
        private Chart _chart;
        private SpriteRenderer _spriteRenderer;
        private TrailRenderer _trailRenderer;
        
        /// <summary>
        /// 空うち時のパーティクル
        /// </summary>
        [SerializeField] private ParticleSystem particle;

        [SerializeField] private Transform center;
        [SerializeField] private Transform groundParent;

        [SerializeField] private Sprite bigBlueSprite;

        private float _offsetSec;


        private int Bpm => _chart.Bpm;
        private float _oneMeasureSec;

        public void Initialize(GameTimer timer, Chart chart, float offsetSec, Color color)
        {
            _songTimeOwner = timer;
            _chart = chart;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _trailRenderer = GetComponent<TrailRenderer>();
            _offsetSec = offsetSec;
            _oneMeasureSec = MusicMath.Get1MeasureSec(Bpm);
            _spriteRenderer.color = color;
            _trailRenderer.startColor = color;
            _trailRenderer.endColor = color;
        }

        private void Update()
        {
            if (_chart == null) return;
            if (_songTimeOwner.SongTimeSec + _oneMeasureSec + _offsetSec <= 0f) return;
            transform.RotateAround(
                center.position,
                Vector3.forward,
                360f * (Time.deltaTime * Bpm) / 240f);
        }

        public void EmitParticle()
        {
            Instantiate(particle, transform.position, Quaternion.identity, groundParent);
        }

        public void ChangeToBigBlueSprite()
        {
            _spriteRenderer.sprite = bigBlueSprite;
        }
    }
}