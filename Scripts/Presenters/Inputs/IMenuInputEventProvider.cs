using System;
using UniRx;

namespace App.Scripts.Presenters.Inputs
{
    public interface IMenuInputEventProvider : IInputEventProvider
    {
        IObservable<bool> Up { get; }
        IObservable<bool> Down { get; }
        IObservable<ArrowInput> Left { get; }
        IObservable<ArrowInput> Right { get; }
        IObservable<bool> Enter { get; }
        IObservable<bool> Tab { get; }
        IObservable<bool> Esc { get; }
    }
}