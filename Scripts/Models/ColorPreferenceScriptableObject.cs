using UnityEngine;

namespace App.Scripts.Models
{
    [CreateAssetMenu]
    public class ColorPreferenceScriptableObject : ScriptableObject
    {
        public Color InnerColor => innerColor;
        [SerializeField] private Color innerColor;
        public Color OuterColor => outerColor;
        [SerializeField] private Color outerColor;
        public Color RotColor => rotColor;
        [SerializeField] private Color rotColor;
    }
}