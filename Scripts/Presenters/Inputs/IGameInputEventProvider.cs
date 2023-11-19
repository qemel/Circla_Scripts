using UniRx;

namespace App.Scripts.Presenters.Inputs
{
    public interface IGameInputEventProvider : IInputEventProvider
    {
        ISubject<bool> Space { get; }
        ISubject<bool> Outer1 { get; }
        ISubject<bool> Inner1 { get; }
        ISubject<bool> Inner2 { get; }
        ISubject<bool> Outer2 { get; }
        ISubject<bool> Retry { get; }
        
        ISubject<bool> Left { get; }
        ISubject<bool> Right { get; }
        ISubject<bool> Up { get; }
        ISubject<bool> Down { get; }
        
        ISubject<bool> Esc { get; }
    }
}