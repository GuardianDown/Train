using System;

namespace Train.Breaking
{
    public interface IRepair : IDisposable
    {
        float CurrentRepairValue { get; }
        float FullRepairValue { get; }

        event Action onFullRepair;
        event Action<float, float> onRepairValueChange;

        void Fix();
    }
}