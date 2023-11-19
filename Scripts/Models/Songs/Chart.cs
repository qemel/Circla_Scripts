using System;
using App.Scripts.Utils;
using UnityEngine;
using LogType = App.Scripts.Utils.LogType;

namespace App.Scripts.Models.Songs.Notes
{
    public class Chart
    {
        public Lane Inner { get; } = new(LaneType.Inner);
        public int InnerCount => Inner.Notes.Count;

        public Lane Outer { get; } = new(LaneType.Outer);
        public int OuterCount => Outer.Notes.Count;

        public Lane InnerRot { get; } = new(LaneType.InnerRot);
        public int InnerRotCount => InnerRot.Notes.Count;

        public Lane OuterRot { get; } = new(LaneType.OuterRot);
        public int OuterRotCount => OuterRot.Notes.Count;

        public Lane Center { get; } = new(LaneType.Center);
        public int CenterCount => Center.Notes.Count;

        public int AllNoteCount => InnerCount + OuterCount + CenterCount + InnerRotCount + OuterRotCount;
        public string SongName { get; }

        /// <summary>
        /// レーン数
        /// </summary>
        public int MaxBlock { get; }

        public int Bpm { get; }
        public int Offset { get; }

        private const float DefaultScaleOfCircleInner = 6.45f;

        public Chart(TextAsset chartTextAsset)
        {
            var json = LoadJson(chartTextAsset);

            SongName = json.name;
            MaxBlock = json.maxBlock;
            Bpm = json.BPM;
            Offset = json.offset;

            JsonToNoteList(json);
        }

        private static ChartNativeJson LoadJson(TextAsset textAsset)
        {
            if (textAsset == null)
            {
                MyLogger.LogLoad("ChartNativeJson is null", LogType.Error);
                return null;
            }

            return JsonUtility.FromJson<ChartNativeJson>(textAsset.ToString());
        }

        /// <summary>
        /// Jsonデータから譜面を解釈し、Notesに格納する
        /// </summary>
        /// <param name="chartNativeJson"></param>
        private void JsonToNoteList(ChartNativeJson chartNativeJson)
        {
            var noteCount = chartNativeJson.notes.Length;

            for (var i = 0; i < noteCount; i++)
            {
                var note = chartNativeJson.notes[i];
                var noteMs = GetNoteMs(note);

                var noteType = GetNoteType(note);
                switch (note.block)
                {
                    case 0:
                        if (Outer.Count >= 1 && IsAlmostSameTime(Outer.Notes[^1].TimeSec, noteMs / 1000f))
                        {
                            MyLogger.LogLoad($"Duplicate note at {noteMs}ms", LogType.Warning);
                            break;
                        }

                        Outer.AddNote(new Note(note.block, noteType, noteMs,
                            GetNotePosition(note, DefaultScaleOfCircleInner), 0));
                        break;
                    case 1:
                        if (Inner.Count >= 1 && IsAlmostSameTime(Inner.Notes[^1].TimeSec, noteMs / 1000f))
                        {
                            MyLogger.LogLoad($"Duplicate note at {noteMs}ms", LogType.Warning);
                            break;
                        }

                        Inner.AddNote(new Note(note.block, noteType, noteMs,
                            GetNotePosition(note, DefaultScaleOfCircleInner), 0));
                        break;
                    case 2:
                        if (OuterRot.Count >= 1 && IsAlmostSameTime(OuterRot.Notes[^1].TimeSec, noteMs / 1000f))
                        {
                            MyLogger.LogLoad($"Duplicate note at {noteMs}ms", LogType.Warning);
                            break;
                        }

                        OuterRot.AddNote(new Note(note.block, noteType, noteMs,
                            Vector2.zero, 90));
                        break;
                    case 3:
                        if (OuterRot.Count >= 1 && IsAlmostSameTime(OuterRot.Notes[^1].TimeSec, noteMs / 1000f))
                        {
                            MyLogger.LogLoad($"Duplicate note at {noteMs}ms", LogType.Warning);
                            break;
                        }

                        OuterRot.AddNote(new Note(note.block, noteType, noteMs,
                            Vector2.zero, 180));
                        break;
                    case 4:
                        if (OuterRot.Count >= 1 && IsAlmostSameTime(OuterRot.Notes[^1].TimeSec, noteMs / 1000f))
                        {
                            MyLogger.LogLoad($"Duplicate note at {noteMs}ms", LogType.Warning);
                            break;
                        }

                        OuterRot.AddNote(new Note(note.block, noteType, noteMs,
                            Vector2.zero, 270));
                        break;
                    case 5:
                        if (OuterRot.Count >= 1 && IsAlmostSameTime(OuterRot.Notes[^1].TimeSec, noteMs / 1000f))
                        {
                            MyLogger.LogLoad($"Duplicate note at {noteMs}ms", LogType.Warning);
                            break;
                        }

                        OuterRot.AddNote(new Note(note.block, noteType, noteMs,
                            Vector2.zero, 0));
                        break;
                    case >= 8 or <= -1:
                        throw new ArgumentOutOfRangeException();
                }
            }

            MyLogger.LogLoad(ToString());
        }

        private static bool IsAlmostSameTime(float time1, float time2)
        {
            return Mathf.Abs(time1 - time2) <= 0.001f;
        }


        /// <summary>
        /// noteの出現時間(ms)
        /// </summary>
        private int GetNoteMs(NoteJson noteJson)
            => (int)(60000f * noteJson.num / (Bpm * (float)noteJson.LPB)) + Offset;


        /// <summary>
        /// ノートの出現位置
        /// </summary>
        private static Vector2 GetNotePosition(NoteJson noteJson, float radiusProportion = 1f)
        {
            var laneNum = noteJson.block;
            var radiusStandard = laneNum switch
            {
                0 => 2f,
                1 => 1f,
                _ => throw new InvalidOperationException()
            };

            var angle = GetNoteAngle(noteJson);
            var radian = Mathf.Deg2Rad * angle;

            var pos = new Vector2(Mathf.Cos(radian) * radiusProportion, Mathf.Sin(radian) * radiusProportion);

            var posRotate90 = new Vector2(-pos.y, pos.x);
            var posRotateMinus90 = new Vector2(pos.y, -pos.x);

            return noteJson.block switch
            {
                0 => posRotate90 * radiusStandard,
                1 => posRotateMinus90 * radiusStandard,
                _ => throw new InvalidOperationException()
            };
        }

        /// <summary>
        /// (0,1)の角度を0としたノートの反時計回りの角度
        /// </summary>
        private static float GetNoteAngle(NoteJson noteJson)
        {
            const float angle = 90f;
            var measure = noteJson.num / (float)noteJson.LPB;
            return Mathf.Repeat(angle * measure, 360);
        }

        private static NoteType GetNoteType(NoteJson noteJson)
        {
            return noteJson.type switch
            {
                1 => NoteType.Tap,
                2 => NoteType.Long,
                _ => throw new InvalidOperationException()
            };
        }

        public override string ToString()
        {
            var str = $"Chart: {SongName}\n" +
                      $"BPM: {Bpm}\n" +
                      $"Offset: {Offset}\n" +
                      $"MaxBlock: {MaxBlock}\n" +
                      $"InnerCount: {InnerCount}\n" +
                      $"OuterCount: {OuterCount}\n" +
                      $"CenterCount: {CenterCount}\n" +
                      $"AllNoteCount: {AllNoteCount}\n";
            return str;
        }
    }
}