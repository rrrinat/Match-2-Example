using System;

namespace Cubie.Reactive
{
    public interface IReadOnlyReactiveTrigger
    {
        IDisposable Subscribe(Action action);
    }
}