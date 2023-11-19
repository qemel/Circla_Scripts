using UnityEngine;

namespace App.Scripts.Models.Songs.Notes
{
    public class NoteSpawnSpeed
    {
        private int _level = 5;
        private float _delaySec => _level / 10f;

        public bool AddSpeed(int addition)
        {
            if (!IsValid(_level + addition)) return false;
            _level += addition;
            PlayerPrefs.SetInt("NoteSpawnSpeed", _level);
            return true;
        }

        public float GetLevel()
        {
            _level = PlayerPrefs.GetInt("NoteSpawnSpeed", 5);
            return _level;
        }

        public float GetSpeed()
        {
            _level = PlayerPrefs.GetInt("NoteSpawnSpeed", 5);
            return _delaySec;
        }

        private bool IsValid(int value)
        {
            return value is >= 0 and <= 10;
        }
    }
}