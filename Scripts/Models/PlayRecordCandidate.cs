using System;
using App.Scripts.Models.Songs.Notes.Plays;
using UnityEngine.InputSystem.LowLevel;

namespace App.Scripts.Models.Songs.Notes
{
    /// <summary>
    /// シーン間で共有するプレイスコア
    /// </summary>
    public static class PlayRecordCandidate
    {
        public static Record Value { get; private set; } = new(0, 0,0, DateTime.Now);
        public static ResultType Type { get; private set; } = ResultType.Normal;

        public static void SetCurrent(Record record, ResultType type = ResultType.Normal)
        {
            Value = record;
            Type = type;
        }
    }
    
    public enum ResultType
    {
        Normal,
        FullCombo,
        Perfect
    }
}