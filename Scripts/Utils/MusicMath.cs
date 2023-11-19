namespace App.Scripts.Utils
{
    public static class MusicMath
    {
        /// <summary>
        /// BPMから1小節の時間を返します
        /// </summary>
        /// <param name="bpm">BPM</param>
        /// <param name="beats">n/4拍子の曲であるときのn</param>
        /// <returns></returns>
        public static int Get1MeasureMs(int bpm, int beats = 4)
        {
            if (bpm <= 0)
            {
                MyLogger.LogSong("BPM is invalid", LogType.Error);
                return 0;
            }

            return 60000 * beats / bpm;
        }
        
        public static float Get1MeasureSec(int bpm, int beats = 4)
        {
            if (bpm <= 0)
            {
                MyLogger.LogSong("BPM is invalid", LogType.Error);
                return 0;
            }

            return 60f * beats / bpm;
        }
    }
}