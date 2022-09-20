using System;

namespace Train.TrainMovement
{
    public interface IPathFollower : IDisposable
    {
        float Input { get; set; }

        void StartFollow();

        void StopFollow();
    }
}