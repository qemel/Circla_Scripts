using UnityEngine;

namespace App.Scripts.Views.UIs.BackGround
{
    public class PlayerBackGroundMover : MonoBehaviour
    {
        [SerializeField] private Vector3 center;

        private float _speed;

        private TrailRenderer _trailRenderer;

        private void Awake()
        {
            transform.RotateAround(
                center,
                Vector3.forward,
                Random.Range(0f, 360f));

            _trailRenderer = GetComponent<TrailRenderer>();
            _trailRenderer.enabled = true;
        }

        private void Update()
        {
            transform.RotateAround(
                center,
                Vector3.forward,
                360f * (Time.deltaTime * _speed) / 240f);
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
    }
}