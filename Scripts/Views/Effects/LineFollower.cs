using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Views.Effects
{
    public class LineFollower : MonoBehaviour
    {
        [SerializeField] private int lineCount;
        [FormerlySerializedAs("renderer")] [SerializeField] private LineRenderer lr;
        private Vector3[] _segmentPositions;
        [SerializeField] private Transform targetTransform;

        private void Start()
        {
            lr.positionCount = lineCount;
            _segmentPositions = new Vector3[lineCount];
        }

        private void Update()
        {
            _segmentPositions[0] = targetTransform.position;

            for (var i = 1; i < _segmentPositions.Length; i++)
            {
                _segmentPositions[i] = Vector3.SmoothDamp(_segmentPositions[i], _segmentPositions[i - 1],
                    ref _segmentPositions[i - 1], 0.5f);
            }

            lr.SetPositions(_segmentPositions);
        }
    }
}