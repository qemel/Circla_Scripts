using App.Scripts.Models;
using App.Scripts.Models.Songs.Notes;
using App.Scripts.Views.UIs.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Presenters.Settings
{
    public class ModVisualPresenter : MonoBehaviour
    {
        [SerializeField] private ModSpriteScriptableObject[] modSpriteScriptableObjects;
        [SerializeField] private ModVisualView modVisualView;

        [SerializeField] private Button upButton;
        [SerializeField] private Button downButton;

        private int _currentModIndex;

        private void Start()
        {
            _currentModIndex = ExtraSettings.ModIndex;
            upButton.onClick.AddListener(Up);
            downButton.onClick.AddListener(Down);
            UpdateModVisual();
        }

        private void Up()
        {
            _currentModIndex++;
            if (_currentModIndex >= modSpriteScriptableObjects.Length) _currentModIndex = 0;
            ExtraSettings.ModIndex = _currentModIndex;
            UpdateModVisual();
        }

        private void Down()
        {
            _currentModIndex--;
            if (_currentModIndex < 0) _currentModIndex = modSpriteScriptableObjects.Length - 1;
            ExtraSettings.ModIndex = _currentModIndex;
            UpdateModVisual();
        }


        private void UpdateModVisual()
        {
            var modSpriteScriptableObject = modSpriteScriptableObjects[_currentModIndex];
            ChoosingMod.SetModVisual(modSpriteScriptableObject.ModOfVisual);
            modVisualView.SetCurrent(modSpriteScriptableObject.Sprite);
        }
        
        private void OnDestroy()
        {
            upButton.onClick.RemoveAllListeners();
            downButton.onClick.RemoveAllListeners();
        }
    }
}