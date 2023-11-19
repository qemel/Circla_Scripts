using System.Linq;
using UnityEngine;

namespace App.Scripts.Views.UIs
{
    public class StarParent : MonoBehaviour
    {
        [SerializeField] private StarView[] stars;

        [SerializeField] private Sprite redPrefab;
        [SerializeField] private Sprite grayPrefab;

        [SerializeField] private Sprite nonePrefab;


        public void SetStar(int level)
        {
            if (level == 0)
            {
                foreach (var starView in stars) starView.Set(nonePrefab);
                return;
            }

            if (level > stars.Length) return;

            stars.Take(level).ToList().ForEach(x => x.Set(redPrefab));
            stars.Skip(level).ToList().ForEach(x => x.Set(grayPrefab));
        }
    }
}