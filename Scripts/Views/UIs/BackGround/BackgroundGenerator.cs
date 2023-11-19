using UnityEngine;

namespace App.Scripts.Views.UIs.BackGround
{
    public class BackgroundGenerator : MonoBehaviour
    {
        [SerializeField] private int trailerCount;
        [SerializeField] private int lineCount;

        [SerializeField] private float scale;
        [SerializeField] private float speedCoefficient;
        [SerializeField] private float trailWidthCoefficient;
        [SerializeField] private float colorDifference;
        [SerializeField] private float lineLifeTime;
        

        [SerializeField] private Color[] colorSet;

        private Color RandomColor => colorSet[Random.Range(0, colorSet.Length)];


        [SerializeField] private PlayerBackGround playerBackGroundPrefab;
        [SerializeField] private LineBackGround lineBackGroundPrefab;


        public void ReGenerate()
        {
            foreach (Transform child in transform) Destroy(child.gameObject);

            for (var i = 0; i < trailerCount; i++)
            {
                var bg = Instantiate(playerBackGroundPrefab, transform);
                bg.transform.localPosition = new Vector3(Random.Range(-scale, scale),
                    Random.Range(-scale, scale), 0f);
                bg.transform.localScale = Vector3.one * Random.Range(1f, 2f);
                var color = RandomColor;
                bg.GetComponent<SpriteRenderer>().color = color;

                var trail = bg.GetComponent<TrailRenderer>();
                trail.startColor = color;
                trail.endColor = new Color(color.r + Random.Range(-colorDifference, colorDifference),
                    color.g + Random.Range(-colorDifference, colorDifference),
                    color.b + Random.Range(-colorDifference, colorDifference));
                trail.startWidth = bg.transform.localScale.x * 0.1f * trailWidthCoefficient;
                trail.endWidth = 0;
                bg.SetSpeed(Random.Range(3f * speedCoefficient, 10f * speedCoefficient));
            }

            for (var i = 0; i < lineCount; i++)
            {
                var bg = Instantiate(lineBackGroundPrefab, transform);
                bg.transform.localPosition = new Vector3(Random.Range(-scale, scale),
                    Random.Range(-scale, scale), 0f);
                bg.transform.localScale = Vector3.one * Random.Range(1f, 2f);
                
                var trail = bg.GetComponent<TrailRenderer>();
                trail.time = Random.Range(0.5f, lineLifeTime);
                bg.SetSpeed(Random.Range(3f * speedCoefficient, 10f * speedCoefficient));
            }
        }
    }
}