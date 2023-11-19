using UnityEngine;

namespace App.Scripts.Views.Entities
{
    public class PlayerRotBarView : MonoBehaviour
    {
        public void Initialize(Color color)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
        }
    }
}