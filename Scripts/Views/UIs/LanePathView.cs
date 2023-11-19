using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class LanePathView : MonoBehaviour
    {
        private Image _image;

        public void Init(Color color)
        {
            TryGetComponent(out _image);
            _image.color = color;
        }
    }
}