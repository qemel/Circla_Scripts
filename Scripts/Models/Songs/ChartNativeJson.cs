using System;
using System.Diagnostics.CodeAnalysis;

namespace App.Scripts.Models.Songs.Notes
{
    /// <summary>
    /// 譜面のJsonデータ
    /// </summary>
    /// <remarks>このクラスはJsonUtility.FromJsonで使用するため、変数名を変更してはいけない</remarks>
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ChartNativeJson
    {
        public string name;
        public int maxBlock;
        public int BPM;
        public int offset;
        public NoteJson[] notes;

        public override string ToString()
        {
            return $"name: {name}, maxBlock: {maxBlock}, BPM: {BPM}, Offset: {offset}, AllNoteCount: {notes.Length}";
        }
    }
}