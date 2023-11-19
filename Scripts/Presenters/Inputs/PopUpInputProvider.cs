using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Scripts.Presenters.Inputs
{
    public class PopUpInputProvider : MonoBehaviour, IPopupInputProvider
    {
        [SerializeField] private PlayerInput input;


        public IObservable<bool> Right => _right;
        private readonly ISubject<bool> _right = new Subject<bool>();
        public IObservable<bool> Left => _left;
        private readonly ISubject<bool> _left = new Subject<bool>();

        private void Initialize()
        {
            input.currentActionMap["Right"].performed += _ => { _right.OnNext(true); };
            input.currentActionMap["Right"].canceled += _ => _right.OnNext(false);
            input.currentActionMap["Left"].performed += _ => { _left.OnNext(true); };
            input.currentActionMap["Left"].canceled += _ => _left.OnNext(false);
        }
    }
}