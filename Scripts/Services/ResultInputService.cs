using App.Scripts.Models.Songs.Notes;
using App.Scripts.Presenters.Inputs;
using UniRx;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public class ResultInputService : MonoBehaviour
    {
        [SerializeField] private MenuInputProvider input;

        public void Initialize()
        {
            input.Enter.DistinctUntilChanged().Where(x => x)
                .Subscribe(_ => { FadeManager.Instance.LoadScene(SceneId.Menu.ToString(), 1f); }).AddTo(this);
        }
    }
}