using App.Scripts.Presenters.Inputs;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static App.Scripts.Views.UIs.PopUpResult;

namespace App.Scripts.Views.UIs
{
    public class DeleteAllRecordsButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private PopUpUIView popUpPrefab;
        [SerializeField] private PopUpSimpleUIView popUpSimplePrefab;
        
        [SerializeField] private Transform popUpParent;

        [SerializeField] private PlayerInput uiInput;


        public void Awake()
        {
            button.onClick.AddListener(() =>
            {
                var popUp = Instantiate(popUpPrefab, popUpParent);
                popUp.Init(uiInput, "本当に削除しますか？", "データを削除すると、ハイスコア・音量設定等の情報が全て初期化されます。本当によろしいでしょうか？").Forget();
                popUp.OnSelected.Where(x => x is Yes).Subscribe(_ =>
                {
                    DeleteAll();
                    Destroy(popUp.gameObject);
                    var popUpSimple = Instantiate(popUpSimplePrefab, popUpParent);
                    popUpSimple.Init("データを削除しました", "削除が完了しました");
                }).AddTo(this);
            });
        }

        private void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}