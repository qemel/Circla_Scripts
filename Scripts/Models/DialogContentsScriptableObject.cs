using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Models.Songs.Notes
{
    [CreateAssetMenu]
    public class DialogContentsScriptableObject : ScriptableObject
    {
        public List<DialogContent> DialogContents => dialogContents;
        [SerializeField] private List<DialogContent> dialogContents;
    }

    [Serializable]
    public class DialogContent
    {
        public float MeasureCount => measureCount;
        [SerializeField] private float measureCount;

        public bool IsActive => isActive;
        [SerializeField] private bool isActive;
        public string Content => content;
        [Multiline] [SerializeField] private string content;

        public Sprite[] KeyImages => keyImages;
        [SerializeField] private Sprite[] keyImages;
    }
}