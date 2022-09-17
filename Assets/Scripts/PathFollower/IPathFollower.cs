using System;

namespace Train.TrainMovement
{
    public interface IPathFollower : IDisposable
    {
        void StartFollow();

        void StopFollow();
    }
}