using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.Models.Songs.Notes
{
    [CreateAssetMenu]
    public class BeatMapScriptableObject : ScriptableObject
    {
        public string Title => title;
        [SerializeField] private string title;
        public Sprite Sprite => sprite;
        [SerializeField] private Sprite sprite;
        public string Artist => artist;
        [SerializeField] private string artist;
        public AudioClip Music => music;
        [SerializeField] private AudioClip music;

        public Color FrameColor => frameColor;
        [SerializeField] private Color frameColor;

        public AudioClip DemoMusic => demoMusic;
        [SerializeField] private AudioClip demoMusic;

        public float AudioVolume => audioVolume;
        [Range(0, 1)] [SerializeField] private float audioVolume = 1;

        [SerializeField] private List<BeatMapInternalInfo> internalInfos;

        public BeatMapInternalInfo FindInfoByType(DifficultyType type)
        {
            if (internalInfos == null) throw new System.Exception("internalInfos is null");
            var res = internalInfos.FirstOrDefault(info => info.Type == type);
            return res;
        }
    }
}