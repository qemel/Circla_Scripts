using UnityEngine;

namespace App.Scripts.Models.Songs.Notes
{
    /// <summary>
    /// NoteJsonのラッパー
    /// </summary>
    public class Note : INote
    {
        public LaneType Lane { get; }
        public NoteType Type { get; }
        public float TimeSec { get; }
        public Vector2 position;
        public float Angle { get; }

        public Note(int laneNumber, NoteType type, int timeMs, Vector2 position, float angle)
        {
            Lane = laneNumber switch
            {
                0 => LaneType.Outer,
                1 => LaneType.Inner,
                2 => LaneType.OuterRot,
                3 => LaneType.InnerRot,
                4 => LaneType.Center,
                _ => LaneType.None
            };
            Type = type;
            TimeSec = timeMs / 1000f;
            this.position = position;
            Angle = angle;
        }

        public override string ToString()
        {
            return $"Lane: {Lane}, DiffType: {Type}, TimeSec: {TimeSec}, position: {position}, Angle: {Angle}";
        }
    }
}