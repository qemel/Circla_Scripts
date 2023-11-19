using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class StarView : MonoBehaviour
    {
        [FormerlySerializedAs("_image")] [SerializeField] private Image image;

        public void Set(Sprite color)
        {
            image.sprite = color;
        }
    }
}