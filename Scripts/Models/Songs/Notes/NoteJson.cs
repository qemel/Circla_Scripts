using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace App.Scripts.Models.Songs.Notes
{
    /// <summary>
    /// NoteのJsonデータ
    /// </summary>
    /// <remarks>このクラスはJsonUtility.FromJsonで使用するため、変数名を変更してはいけない</remarks>
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class NoteJson
    {
        /// <summary>
        /// 流れてくる時間の指標
        /// </summary>
        public int num;

        /// <summary>
        /// レーン番号
        /// 0: 内側レーン
        /// 1: 外側レーン
        /// 2: 中央レーン
        /// </summary>
        public int block;

        /// <summary>
        /// 1小節あたりの分割数（Line Per Bar）
        /// LPB = 1 のときは 4 分割 (4分の4拍子で分割される)
        /// </summary>
        public int LPB;

        /// <summary>
        /// ノートの種類
        /// 1: タップノート
        /// 2: ロングノート
        /// </summary>
        public int type;

        public float angle;
    }
}