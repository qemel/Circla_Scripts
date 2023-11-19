using UnityEngine;

namespace App.Scripts.Models.Songs.Notes
{
    public class RotNoteSpeed
    {
        private int _level = 5;
        private float _delaySec => _level / 5f;

        public bool AddSpeed(int addition)
        {
            if (!IsValid(_level + addition)) return false;
            _level += addition;
            PlayerPrefs.SetInt("RotNoteSpeed", _level);
            return true;
        }
        
        public float GetLevel()
        {
            _level = PlayerPrefs.GetInt("RotNoteSpeed", 5);
            return _level;
        }

        public float GetSpeed()
        {
            _level = PlayerPrefs.GetInt("RotNoteSpeed", 5);
            return _delaySec;
        }

        private bool IsValid(int level)
        {
            return level is >= 0 and <= 10;
        }
    }
}