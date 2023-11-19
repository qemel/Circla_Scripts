using System.Collections.Generic;

namespace App.Scripts.Models.Songs.Notes
{
    /// <summary>
    /// Laneごとのノーツを管理するクラス
    /// </summary>
    public class Lane
    {
        public List<Note> Notes { get; private set; }

        private int _index = 0;

        public LaneType Type { get; }

        public Lane(LaneType type)
        {
            Type = type;
            Notes = new List<Note>();
        }

        public int Count => Notes.Count;

        public void AddNote(Note note)
        {
            Notes.Add(note);
        }

        public Note Current()
        {
            if (_index >= Notes.Count) return null;
            return Notes[_index];
        }

        public Note NextNote()
        {
            if (_index >= Notes.Count) return null;
            return Notes[_index++];
        }
    }
}