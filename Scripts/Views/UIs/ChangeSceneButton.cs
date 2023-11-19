using App.Scripts.Models.Songs.Notes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.Views.UIs
{
    public class ChangeSceneButton : MonoBehaviour
    {
        private Button _button;
        [SerializeField] private SceneId sceneId;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => { FadeManager.Instance.LoadScene(sceneId.ToString(), 1f); });
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}