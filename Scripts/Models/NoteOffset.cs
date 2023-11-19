using UnityEngine;

namespace App.Scripts.Models
{
    public class NoteOffset
    {
        /// <summary>
        /// - -> 判定が早くなる
        /// + -> 判定が遅くなる
        /// </summary>
        private int OffsetMs { get; set; }
        
        private float OffsetSec => OffsetMs / 1000f;

        public bool AddByMs(int ms)
        {
            if (!IsValid(OffsetMs + ms)) return false;
            OffsetMs += ms;
            PlayerPrefs.SetInt("NoteJudgeTimeOffset", OffsetMs);
            return true;
        }
        
        public int GetMs()
        {
            OffsetMs = PlayerPrefs.GetInt("NoteJudgeTimeOffset", 0);
            return OffsetMs;
        }
        
        public float GetSec()
        {
            OffsetMs = PlayerPrefs.GetInt("NoteJudgeTimeOffset", 0);
            return OffsetSec;
        }

        private static bool IsValid(int amount)
        {
            return Mathf.Abs(amount) <= 200;
        }
    }
}