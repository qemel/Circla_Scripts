using App.Scripts.Models.Songs.Notes;
using App.Scripts.Views.UIs;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public class TutorialDialogs : MonoBehaviour
    {
        [SerializeField] private GameTimer timer;
        [SerializeField] private DialogView dialogView;

        private float SongTimeSec => timer.SongTimeSec;

        [SerializeField] private DialogContentsScriptableObject dialogContentsScriptableObject;

        private int _index;

        private void Update()
        {
            if (_index >= dialogContentsScriptableObject.DialogContents.Count) return;
            if (SongTimeSec <= dialogContentsScriptableObject.DialogContents[_index].MeasureCount) return;

            dialogView.Show(dialogContentsScriptableObject.DialogContents[_index]);

            _index++;
        }
    }
}