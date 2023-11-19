using UnityEngine;

namespace App.Scripts.Models.Songs.Notes
{
    [CreateAssetMenu]
    public class RankScriptableObject : ScriptableObject
    {
        public int LowerBound => lowerBound;
        [SerializeField] private int lowerBound;
        public Sprite Sprite => sprite;
        [SerializeField] private Sprite sprite;
    }
}