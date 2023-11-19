using System;
using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Judgements;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Views.Entities
{
    public class RotNoteView : MonoBehaviour, INoteView
    {
        [FormerlySerializedAs("renderer")] [SerializeField]
        private SpriteRenderer rend;

        [SerializeField] private ParticleSystem hitParticle;

        [Range(0, 1)] [SerializeField] private float scaleOut;
        [SerializeField] private float durationOut;

        [SerializeField] private AudioClip hitClip;

        public float Radius { get; private set; }
        public bool IsOuter { get; set; }

        private readonly Vector2 CenterPosition = new(0, 0);
        private const float Margin = 0.3f;
        private Vector2 _direction;

        public void Initialize(float angle, bool isOuter, Color color, float time, Transform parent)
        {
            gameObject.SetActive(true);
            _direction = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
            transform.position = CenterPosition;
            rend.color = color;
            IsOuter = isOuter;
            Radius = isOuter ? (12.8f + Margin) : (12.8f + Margin) / 2f;

            transform.SetParent(parent);
            DoInitAnimation(time);
        }

        public void SetDirection(float angle)
        {
            _direction = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
        }

        public void DoInitAnimation(float time)
        {
            gameObject.SetActive(true);
            transform.localScale = new Vector3(scaleOut, scaleOut, 1);
            transform.DOScale(new Vector3(1, 1, 1), durationOut);
            transform.DOMove(_direction * Radius, time).SetEase(Ease.Linear);
        }

        public void DoDestroyAnimation(IJudgement type)
        {
            if (type is Miss or null)
            {
                transform.DOScale(new Vector3(scaleOut, scaleOut, 1), durationOut * 1.2f);
                rend.DOFade(0, durationOut * 1.2f).SetLink(gameObject).onComplete += () => Destroy(gameObject);
                return;
            }

            LucidAudio.PlaySE(hitClip).SetTimeSamples(4400);
            Instantiate(hitParticle, transform.position, Quaternion.identity, transform.parent);
            transform.DOScale(new Vector3(scaleOut, scaleOut, 1), durationOut);
            rend.DOFade(0, durationOut).onComplete += () => gameObject.SetActive(false);
        }
    }
}