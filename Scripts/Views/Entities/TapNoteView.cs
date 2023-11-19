using System;
using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Judgements;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Views.Entities
{
    public class TapNoteView : MonoBehaviour, INoteView
    {
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private AudioClip hit;

        [SerializeField] private ParticleSystem particlePerfect;
        [SerializeField] private ParticleSystem particleGreat;
        [SerializeField] private ParticleSystem particleGood;
        [SerializeField] private ParticleSystem particleBad;

        [Range(0, 1)] [SerializeField] private float scaleInit;
        [Range(0, 1)] [SerializeField] private float scaleIn;
        [Range(0, 2)] [SerializeField] private float scaleOut;

        [SerializeField] private float durationIn;
        [SerializeField] private float durationOut;

        public void Init(Color color, Transform parent)
        {
            gameObject.SetActive(true);
            renderer.color = color;
            transform.SetParent(parent);
            transform.localScale = new Vector3(scaleInit, scaleInit, 1);
            transform.DOScale(new Vector3(scaleIn, scaleIn, 1), durationIn);
        }

        public void DoDestroyAnimation(IJudgement type)
        {
            if (type is Miss or null)
            {
                transform.DOScale(new Vector3(scaleOut, scaleOut, 1), durationOut * 1.2f);
                renderer.DOFade(0, durationOut * 1.2f).onComplete += () => Destroy(gameObject);
                return;
            }

            LucidAudio.PlaySE(hit).SetTimeSamples(4400);
            Instantiate(GetParticleByType(type), transform.position, Quaternion.identity, transform.parent);
            transform.DOScale(new Vector3(scaleOut, scaleOut, 1), durationOut);
            renderer.DOFade(0, durationOut).onComplete += () => gameObject.SetActive(false);
        }

        private ParticleSystem GetParticleByType(IJudgement type)
        {
            return type switch
            {
                Perfect => particlePerfect,
                Great => particleGreat,
                Good => particleGood,
                Bad => particleBad,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}