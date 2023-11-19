using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class RotateView : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }
}