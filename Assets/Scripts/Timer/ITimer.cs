using System;

namespace Train.Timer
{
    public interface ITimer : IDisposable
    {
        event Action<int> onTimeChange;
        event Action onTimeEnd;

        void StartTimer();
        void StopTimer();
    }
}