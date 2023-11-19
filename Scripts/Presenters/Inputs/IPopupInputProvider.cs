using System;

namespace App.Scripts.Presenters.Inputs
{
    public interface IPopupInputProvider
    {
        IObservable<bool> Right { get; }
        IObservable<bool> Left { get; }
    }
}