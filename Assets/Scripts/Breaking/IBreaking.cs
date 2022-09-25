using System;

namespace Train.Breaking
{
    public interface IBreaking : IDisposable
    {
        event Action<float> onBreaking;

        void StartBreaking();
        void StopBreaking();
    }
}