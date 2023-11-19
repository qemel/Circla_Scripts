using App.Scripts.Models.Mods;
using UnityEngine;

namespace App.Scripts.Models.Songs.Notes
{
    [CreateAssetMenu]
    public class ModSpriteScriptableObject : ScriptableObject
    {
        public Sprite Sprite => sprite;
        [SerializeField] private Sprite sprite;
        public IModOfVisual ModOfVisual => _modOfVisual;
        [SerializeReference] private IModOfVisual _modOfVisual;
    }
}