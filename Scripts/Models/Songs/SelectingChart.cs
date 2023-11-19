using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Models.Songs.Notes
{
    /// <summary>
    /// 選択中の譜面の情報をシーン間で保持する
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class SelectingBeatMapSO
    {
        public static string Title { get; private set; }
        public static DifficultyType DifficultyType { get; private set; }
        [CanBeNull] public static TextAsset ChartTextAsset { get; private set; }
        public static AudioClip Music { get; private set; }
        public static Sprite Sprite { get; private set; }

        public static void SetCurrent(BeatMapScriptableObject beatMapScriptableObject, DifficultyType difficultyType)

        {
            Title = beatMapScriptableObject.Title;
            DifficultyType = difficultyType;
            if (beatMapScriptableObject.FindInfoByType(difficultyType) != null)
                ChartTextAsset = beatMapScriptableObject.FindInfoByType(difficultyType).ChartTextAsset;
            Music = beatMapScriptableObject.Music;
            Sprite = beatMapScriptableObject.Sprite;
        }
    }
}