using System;
using UnityEngine;

namespace App.Scripts.Models.Songs.Notes
{
    [Serializable]
    public class BeatMapInternalInfo
    {
        public DifficultyType Type => type;
        [SerializeField] private DifficultyType type;
        public int Level => level;
        [SerializeField] private int level;
        public TextAsset ChartTextAsset => chartTextAsset;
        [SerializeField] private TextAsset chartTextAsset;
        
        public override string ToString()
        {
            return $"Type: {type}, Level: {level}";
        }
    }
}