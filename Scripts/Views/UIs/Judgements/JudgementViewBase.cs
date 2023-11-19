using System;
using App.Scripts.Models.Judgements;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views.UIs.Judgements
{
    public class JudgementViewBase : MonoBehaviour, IJudgementView
    {
        public IJudgement Type => type;
        [SerializeReference] protected IJudgement type;

        [SerializeField] protected TextMeshProUGUI text;

        private void Awake()
        {
            Reset();
        }

        public void SetJudge(int count)
        {
            text.text = count.ToString();
        }

        public void Reset()
        {
            SetJudge(0);
        }
    }
}